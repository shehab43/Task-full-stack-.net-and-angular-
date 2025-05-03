export interface ApiResponse<T> {
  isSuccess: boolean;
  data: T;
  statusCode: number;
  message: string;
}

export interface Employee {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  position: string;
}