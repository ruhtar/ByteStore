import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FooterModule } from './components/footer/footer.module';
import { FormModule } from './components/form/form.module';
import { HeaderModule } from './components/header/header.module';
import { LoginComponent } from './components/login/login.component';
import { CartComponent } from './components/main-page/cart/cart.component';
import { ContactComponent } from './components/main-page/contact/contact.component';
import { HomeComponent } from './components/main-page/home/home.component';
import { InfoModule } from './components/main-page/user-settings/info/info.module';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { SignupModule } from './components/signup/signup.module';
import { AuthService } from './services/auth/auth.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    CartComponent,
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
  ],
  providers: [AuthService],
  bootstrap: [AppComponent],
})
export class AppModule {}
