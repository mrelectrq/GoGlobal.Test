import {Component, OnInit} from '@angular/core'
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {User} from '../../help/interfaces';
import {AuthService} from '../../services/auth.service';
import {ActivatedRoute, Params, Router} from '@angular/router';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {

  form: FormGroup
  submitted = false
  message: string

  constructor(
    public auth: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {
  }

  ngOnInit() {
    this.route.queryParams.subscribe((params: Params) => {
      if (params['loginAgain']) {
        this.message = 'Please enter data'
      } else if (params['authFailed']) {
        this.message = 'The session has expired. Re-enter the data.'
      }
    })

    this.form = new FormGroup({
      username: new FormControl(null, [
        Validators.required,
      ]),
      password: new FormControl(null, [
        Validators.required,
        Validators.minLength(4)
      ])
    })
  }

  submit() {
    if (this.form.invalid) {
      return
    }

    this.submitted = true
    const user: User = {
      username: this.form.value.username,
      password: this.form.value.password
    }

    this.auth.login(user).subscribe((resp) => {
      console.log(resp)
      this.setToken(resp)
      this.form.reset()
      this.router.navigate(['/cards'])
      this.submitted = false
    }, () => {
      this.submitted = false
    })
  }

  private setToken(response) {
    if (response) {
      localStorage.setItem('cookie', response.tokenValue)
    } else {
      localStorage.clear()
    }
  }

}
