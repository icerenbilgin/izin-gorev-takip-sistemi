using api.Core.Utilities.Results.Abstract;
using api.Core.Utilities.Results.Concrete;
using api.Data;
using AutoMapper;
using api.Core.Utilities.Results;
using api.Dtos;
using api.Models;
using api.Helpers;
using api.Enums;
using Microsoft.AspNetCore.SignalR;
using api.Hubs;

public class LeaveRequestsManager : ILeaveRequestService
{
    private readonly AppDbContext _context;
    private readonly ILeaveRequestsDal _leaveRequestsDal;
    private readonly IMapper _mapper;
    private readonly IHubContext<NotificationHub> _hubContext;

    public LeaveRequestsManager(
        AppDbContext context,
        ILeaveRequestsDal leaveRequestsDal,
        IMapper mapper,
        IHubContext<NotificationHub> hubContext)
    {
        _context = context;
        _leaveRequestsDal = leaveRequestsDal;
        _mapper = mapper;
        _hubContext = hubContext;
    }

    public IDataResult<LeaveRequestsDto> AddOrUpdateLeaveRequest(LeaveRequestsDto leaveRequestsDto)
    {
        try
        {
            if (leaveRequestsDto.LeaveRequestId == 0)
            {
                var entity = _mapper.Map<LeaveRequests>(leaveRequestsDto);
                entity.IsActive = true;

                _context.LeaveRequests.Add(entity);
                _context.SaveChanges();

                var dto = _mapper.Map<LeaveRequestsDto>(entity);
                return new SuccessDataResult<LeaveRequestsDto>(dto, "Kayıt eklendi.");
            }
            else
            {
                var entity = _context.LeaveRequests
                    .FirstOrDefault(x => x.LeaveRequestId == leaveRequestsDto.LeaveRequestId);

                if (entity == null)
                    return new ErrorDataResult<LeaveRequestsDto>("Kayıt bulunamadı.");

                _mapper.Map(leaveRequestsDto, entity);

                _context.LeaveRequests.Update(entity);
                _context.SaveChanges();

                var dto = _mapper.Map<LeaveRequestsDto>(entity);
                return new SuccessDataResult<LeaveRequestsDto>(dto, "Kayıt güncellendi.");
            }
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<LeaveRequestsDto>(
                $"Ekleme / güncelleme sırasında hata oluştu: {ex.Message}"
            );
        }
    }

    public IDataResult<CheckLeaveRequestDto> CheckLeaveRequest(
    CheckLeaveRequestDto checkLeaveRequestDto)
    {
        try
        {
            if (checkLeaveRequestDto.LeaveRequestId <= 0)
                return new ErrorDataResult<CheckLeaveRequestDto>(
                    "Geçersiz izin kaydı."
                );

            var leaveRequest = _context.LeaveRequests
                .FirstOrDefault(x => x.LeaveRequestId == checkLeaveRequestDto.LeaveRequestId);

            if (leaveRequest == null)
                return new ErrorDataResult<CheckLeaveRequestDto>(
                    "İzin talebi bulunamadı."
                );

            string approvedByRoleName;

            switch (checkLeaveRequestDto.ApprovedByRoleId)
            {
                case (int)UserRoleEnum.TakimLideri:
                    leaveRequest.TeamLeaderId = checkLeaveRequestDto.ApprovedByUserId;
                    leaveRequest.TeamLeaderApprovalDate = DateTime.Now;
                    approvedByRoleName = "Takım Lideri";
                    break;

                case (int)UserRoleEnum.InsanKaynaklariUzmani:
                    leaveRequest.HrSpecialistId = checkLeaveRequestDto.ApprovedByUserId;
                    leaveRequest.HrSpecialistApprovalDate = DateTime.Now;
                    approvedByRoleName = "İnsan Kaynakları Uzmanı";
                    break;

                case (int)UserRoleEnum.Yonetici:
                    leaveRequest.SeniorManagerId = checkLeaveRequestDto.ApprovedByUserId;
                    leaveRequest.SeniorManagerApprovalDate = DateTime.Now;
                    approvedByRoleName = "Yönetici";
                    break;

                default:
                    return new ErrorDataResult<CheckLeaveRequestDto>(
                        "Yetkisiz işlem."
                    );
            }

            _context.LeaveRequests.Update(leaveRequest);
            _context.SaveChanges();

            _hubContext.Clients
                .Group($"user-{leaveRequest.UserId}")
                .SendAsync(
                    "ReceiveNotification",
                    $"İzin talebiniz {approvedByRoleName} tarafından onaylandı."
                );

            return new SuccessDataResult<CheckLeaveRequestDto>(
                checkLeaveRequestDto,
                "İzin başarıyla onaylandı."
            );
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<CheckLeaveRequestDto>(
                $"İşlem sırasında hata oluştu: {ex.Message}"
            );
        }
    }

    public api.Core.Utilities.Results.Abstract.IResult DeleteLeaveRequest(int id)
    {
        try
        {
            var entity = _context.LeaveRequests
                .FirstOrDefault(x => x.LeaveRequestId == id);

            if (entity == null)
                return new ErrorResult("Kayıt bulunamadı.");

            entity.IsActive = false;

            _context.LeaveRequests.Update(entity);
            _context.SaveChanges();

            return new SuccessResult("Kayıt pasif hale getirildi.");
        }
        catch (Exception ex)
        {
            return new ErrorResult($"Pasife alma sırasında hata oluştu: {ex.Message}");
        }
    }

    public IDataResult<List<LeaveRequestsDto>> GetAllLeaveRequest()
    {
        try
        {
            var data = _leaveRequestsDal.GetAllLeaveRequest();

            var dto = data
                .Select(LeaveRequestsHelper.ToDto)
                .ToList();

            return new SuccessDataResult<List<LeaveRequestsDto>>(dto);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<LeaveRequestsDto>>(
                $"Kayıtlar getirilirken hata oluştu: {ex.Message}"
            );
        }
    }

    public IDataResult<LeaveRequestsDto> GetLeaveRequestById(int id)
    {
        try
        {
            var entity = _leaveRequestsDal.GetLeaveRequestById(id);

            if (entity == null)
                return new ErrorDataResult<LeaveRequestsDto>("İzin talebi bulunamadı.");

            var dto = LeaveRequestsHelper.ToDto(entity);

            return new SuccessDataResult<LeaveRequestsDto>(dto);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<LeaveRequestsDto>(
                $"İzin talebi getirilirken hata oluştu: {ex.Message}"
            );
        }
    }

    public IDataResult<List<LeaveRequestsDto>> GetLeaveRequestByUserId(int userId)
    {
        try
        {
            var entities = _leaveRequestsDal.GetLeaveRequestByUserId(userId);

            var dtoList = entities
                .Select(LeaveRequestsHelper.ToDto)
                .ToList();

            return new SuccessDataResult<List<LeaveRequestsDto>>(dtoList);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<LeaveRequestsDto>>(
                $"İzin talepleri getirilirken hata oluştu: {ex.Message}"
            );
        }
    }
}