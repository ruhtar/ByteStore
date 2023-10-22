import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FooterModule } from './components/footer/footer.module';
import { FormModule } from './components/form/form.module';
import { HeaderModule } from './components/header/header.module';
import { LoginComponent } from './components/login/login.component';
import { ContactComponent } from './components/main-page/contact/contact.component';
import { HomeComponent } from './components/main-page/home/home.component';
import { InfoModule } from './components/main-page/user-settings/info/info.module';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { SignupModule } from './components/signup/signup.module';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    ContactComponent,
    PageNotFoundComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    FooterModule,
    HeaderModule,
    FormModule,
    SignupModule,
    InfoModule,
    [SweetAlert2Module.forRoot()],
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
