import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse , Employee} from '../models/employee.model';
@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private apiUrl = 'https://localhost:7004/api/Employee';

  constructor(private http: HttpClient) { }

  getAllEmployees(): Observable<ApiResponse<Employee[]>> {
    return this.http.get<ApiResponse<Employee[]>>(this.apiUrl);
}

getEmployee(id: number)
{
  return this.http.get<Employee>(`${this.apiUrl}id:/${id}`);
}

  createEmployee(AddEmployee: Employee) {
    return this.http.post<Employee>(this.apiUrl, AddEmployee);
  }

  updateEmployee(employee: Employee): Observable<ApiResponse<boolean>> {
    return this.http.put<ApiResponse<boolean>>(`${this.apiUrl}`, employee);
  }

  deleteEmployee(id: number): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(`${this.apiUrl}?id=${id}`);
}
}