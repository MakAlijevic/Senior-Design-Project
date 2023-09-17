import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Country } from '../model/country.model';
import { UserRegister } from '../model/user.register.model';
import { AuthServiceService } from '../service/auth-service.service';

@Component({
  selector: 'app-register-modal',
  templateUrl: './register-modal.component.html',
  styleUrls: ['./register-modal.component.css']
})
export class RegisterModalComponent implements OnInit {

  public countries: Country[];

  username: string;
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  gender: string;
  address: string;
  countryId = 0;

  constructor(private http: HttpClient, private authService: AuthServiceService) {
  }

  ngOnInit(): void {
    this.loadCountries();
  }

  loadCountries() {
    this.http.get<any>('https://localhost:7063/api/Country').subscribe(countries => {
      this.countries = countries;
    });
  }

  register() {
    var registeredUser = new UserRegister();
    registeredUser.firstName = this.firstName;
    registeredUser.lastName = this.lastName;
    registeredUser.email = this.email;
    registeredUser.address = this.address;
    registeredUser.gender = this.gender;
    registeredUser.username = this.username;
    registeredUser.password = this.password;
    registeredUser.countryId = this.countryId;

    this.authService.register(registeredUser);
  }
}
