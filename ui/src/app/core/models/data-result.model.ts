export interface DataResultModel<T> {
  data: T;
  success: boolean;
  message: string;
}