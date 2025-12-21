export interface TasksDto {
  taskId?: number;

  createdUserId?: number;
  assignedUserId?: number;

  taskContent?: string;

  taskStatusId?: number;

  createdDate: string | Date;
  startedDate?: string | Date;
  finishedDate?: string | Date;
  assignedDate?: string | Date;

  isActive?: boolean;
}