import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginService } from './services/login/login.service';
import { HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { HomeComponent } from './components/main-page/home/home.component';
import { ProductsComponent } from './components/main-page/products/products.component';
import { CartComponent } from './components/main-page/cart/cart.component';
import { ContactComponent } from './components/main-page/contact/contact.component';
import { FormComponent } from './components/form/form.component';
import { RegisterFormComponent } from './components/register-form/register-form.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    ProductsComponent,
    CartComponent,
    ContactComponent,
    FormComponent,
    RegisterFormComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [LoginService],
  bootstrap: [AppComponent],
})
export class AppModule {}
