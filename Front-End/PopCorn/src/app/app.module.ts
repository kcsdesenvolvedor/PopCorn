import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HeaderComponent } from './template/header/header.component';
import { NavComponent } from './template/nav/nav.component';
import { FooterComponent } from './template/footer/footer.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule,} from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatListModule } from '@angular/material/list';   
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { ListMoviesComponent } from './components/movies/list-movies/list-movies.component';
import { FormMoviesComponent } from './components/movies/form-movies/form-movies.component';
import { RoomsComponent } from './components/rooms/rooms.component';
import { DialogInfo, ListSessionsComponent } from './components/sessions/list-sessions/list-sessions.component';
import { FormSessionsComponent } from './components/sessions/form-sessions/form-sessions.component';
import { ListUsersComponent } from './components/users/list-users/list-users.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ɵINTERNAL_BROWSER_DYNAMIC_PLATFORM_PROVIDERS } from '@angular/platform-browser-dynamic';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { TokenInterceptor } from './tokenInterceptors/token.interceptor';
import { MatDialogModule } from '@angular/material/dialog';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { AlifeFileToBase64Module } from 'alife-file-to-base64';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { LoadComponent } from './components/load/load.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatRadioModule } from '@angular/material/radio';
import { MatNativeDateModule, MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { CurrencyMaskConfig, CurrencyMaskModule, CURRENCY_MASK_CONFIG } from 'ng2-currency-mask';
import { FormUsersComponent } from './components/users/form-users/form-users.component';

registerLocaleData(localePt);

export const CustomCurrencyMaskConfig: CurrencyMaskConfig = {
  align: "right",
    allowNegative: true,
    decimal: ",",
    precision: 2,
    prefix: "R$ ",
    suffix: "",
    thousands: "."
};

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    NavComponent,
    FooterComponent,
    ListMoviesComponent,
    FormMoviesComponent,
    RoomsComponent,
    ListSessionsComponent,
    FormSessionsComponent,
    ListUsersComponent,
    LoginComponent,
    DialogInfo,
    LoadComponent,
    FormUsersComponent
    
  ],
  imports: [
    BrowserModule,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NoopAnimationsModule,
    MatCardModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatSnackBarModule,
    MatButtonModule,
    MatInputModule,
    MatTableModule,
    MatCardModule,
    MatDialogModule,
    AlifeFileToBase64Module,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    MatOptionModule,
    MatRadioModule,
    CurrencyMaskModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
  ],
  providers: [
    ɵINTERNAL_BROWSER_DYNAMIC_PLATFORM_PROVIDERS,
    {
      provide: LOCALE_ID,
      useValue: 'pt-Br',
    },
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    { provide: CURRENCY_MASK_CONFIG, useValue: CustomCurrencyMaskConfig }
  ],
  bootstrap: [AppComponent,]
})
export class AppModule { }
