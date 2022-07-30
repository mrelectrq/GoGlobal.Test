import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Provider } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { BookmarksComponent } from './components/bookmarks/create-page.component';
import { CardsPageComponent } from './components/cards-page/cards-page.component';
import { MainPageComponent } from './components/main-page/main-page.component';
import { AuthInterceptor } from './services/auth.interceptor';
import { AlertComponent } from './components/alert/alert.component';
import { AlertService } from './services/alert.service';
import { AuthGuard } from './services/auth.guars';
import { PostsService } from './services/posts.service';
import { AuthService } from './services/auth.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

const INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  multi: true,
  useClass: AuthInterceptor,
};

@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    BookmarksComponent,
    CardsPageComponent,
    MainPageComponent,
    AlertComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
  ],
  providers: [
    AlertService,
    AuthGuard,
    PostsService,
    AuthService,
    INTERCEPTOR_PROVIDER,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
