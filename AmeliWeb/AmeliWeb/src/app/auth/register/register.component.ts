import { Component, OnInit } from '@angular/core';
import { CustomErrorStateMatcher } from '../../shared/customErrorStateMatcher';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  username = '';
  password = '';
  passwordRepeat = '';
  isLoadingResults = false;
  matcher = new CustomErrorStateMatcher();

  constructor(
    private authService: AuthService,
    private router: Router,
    private formBuilder: FormBuilder
  ) { }

  //alternate implementation to login.component. Is this equal? ToDo: verify
  ngOnInit(): void {
    this.registerForm = new FormGroup({
      username: new FormControl(this.username, [
        Validators.required,
        Validators.minLength(4)
      ]),
      password: new FormControl(this.password, [
        Validators.required,
        Validators.minLength(4)
      ]),
      passwordRepeat: new FormControl(this.passwordRepeat, [
        Validators.required,
        Validators.pattern(this.password) //<- don't know yet, if this works. Alternative: validation in markup?
      ]),
    });
  }

  onFormSubmit(): void {
    this.isLoadingResults = true;
    this.authService.register(this.registerForm.value)
      .subscribe((res: any) => {
        this.isLoadingResults = false;
        this.router.navigate(['/login']).then(_ => console.log('Registration successful'));
      }, (err: any) => {
        console.log(err);
        this.isLoadingResults = false;
      });
  }

}
