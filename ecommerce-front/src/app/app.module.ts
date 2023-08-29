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
import { MainPageComponent } from './components/main-page/main-page.component';
import { HomeComponent } from './components/main-page/home/home.component';
import { ProductsComponent } from './components/main-page/products/products.component';
import { CartComponent } from './components/main-page/cart/cart.component';
import { ContactComponent } from './components/main-page/contact/contact.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HeaderComponent,
    FooterComponent,
    MainPageComponent,
    HomeComponent,
    ProductsComponent,
    CartComponent,
    ContactComponent,
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
