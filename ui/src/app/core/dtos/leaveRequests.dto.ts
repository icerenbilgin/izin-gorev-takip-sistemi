export interface LeaveRequestsDto {
  leaveRequestId?: number;

  userId?: number;
  leaveTypeId?: number;

  startedDate?: string | Date;
  finishDate?: string | Date;

  teamLeaderId?: number;
  teamLeaderApprovalDate?: string | Date;

  hrSpecialistId?: number;
  hrSpecialistApprovalDate?: string | Date;

  seniorManagerId?: number;
  seniorManagerApprovalDate?: string | Date;

  rejectionStatement?: string;

  rejectingUserId?: number;

  dayCount?: number;

  isActive?: boolean;
}