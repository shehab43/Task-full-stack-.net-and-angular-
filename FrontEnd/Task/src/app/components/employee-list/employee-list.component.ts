import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/employee.service';
import { Employee } from 'src/app/models/employee.model';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  employees: Employee[] = [];

  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.employeeService.getAllEmployees()
      .subscribe(response => {
        if (response.isSuccess) {
          this.employees = response.data;  
        }
      });
  }

  deleteEmployee(id: number): void {
    if(confirm('Are you sure you want to delete this employee?')) {
        this.employeeService.deleteEmployee(id)
            .subscribe({
                next: (response) => {
                    if(response.isSuccess && response.data === true) {
                        alert(response.message); // Optional: show success message
                        this.loadEmployees();
                    }
                },
                error: (error) => {
                    console.error('Error deleting employee:', error);
                    alert('Failed to delete employee');
                }
            });
    }
}}