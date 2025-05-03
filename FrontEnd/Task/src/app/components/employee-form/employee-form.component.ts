import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.css']
})
export class EmployeeFormComponent implements OnInit {
  employeeForm: FormGroup;
  isEditMode = false;
  employeeId: number = 0;  

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.employeeForm = this.fb.group({
      FirstName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      LastName: ['', Validators.required],
      position: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      if (params['id']) {
        this.isEditMode = true;
         this.employeeId = +params['id'];
        this.loadEmployee();
      }
    });
  }

  loadEmployee(): void {
    this.employeeService.getEmployee(this.employeeId)
      .subscribe(employee => {
        this.employeeForm.patchValue(employee);
      });
  }

  onSubmit(): void {
    if (this.employeeForm.valid) {
      if (this.isEditMode) {
        const updatedEmployee = { ...this.employeeForm.value, id: this.employeeId };
        console.log('Updated Employee Data:', updatedEmployee);
        this.employeeService.updateEmployee(updatedEmployee).subscribe({
          next: (response) => {
            if (response.isSuccess) {
              this.router.navigate(['/employees']);
            }
          },
          error: (error) => {
            console.error('Update error:', error);
          }
        })
    }
  }
}}