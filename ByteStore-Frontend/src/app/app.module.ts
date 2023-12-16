import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatPaginatorModule } from '@angular/material/paginator';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormModule } from './components/form/form.module';
import { LoginComponent } from './components/login/login.component';
import { ContactComponent } from './components/main-page/contact/contact.component';
import { HomeComponent } from './components/main-page/home/home.component';
import { InfoModule } from './components/main-page/user-settings/info/info.module';
import { SignupModule } from './components/signup/signup.module';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { HttpInterceptorService } from './services/http-interceptor/http-interceptor.service';
import { FooterModule } from './shared/footer/footer.module';
import { HeaderModule } from './shared/header/header.module';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    ContactComponent,
    PageNotFoundComponent,
  ],
  imports: [
    MatPaginatorModule,
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    FooterModule,
    HeaderModule,
    FormModule,
    SignupModule,
    InfoModule,
    MatDialogModule,
    [SweetAlert2Module.forRoot()],
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
