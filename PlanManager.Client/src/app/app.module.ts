import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ConfirmComponent } from './components/confirm/confirm.component';
import { MainComponent } from './pages';
import { NavigatorComponent } from './layout';
import { FooterComponent } from './layout';
import { UtilsService } from './utils';
import { AuthService, NotificationService, StateService } from './services';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material/material.module';
import { ToastrModule } from 'ngx-toastr';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HomeComponent } from './pages/home/home.component';
import { PlanManagerHomeComponent } from './pages/pm/plan-manager-home/plan-manager-home.component';
import {
  AuthComponent,
  AuthLoginComponent,
  AuthRegistrationComponent,
  AuthPasswordResetComponent,
} from './pages/auth';
import { LeadZeroPipe } from './pipes/lead-zero.pipe';
import { PmPlansComponent } from './pages/pm/pm-plans/pm-plans.component';
import { PmPlanComponent } from './pages/pm/pm-plans/pm-plan/pm-plan.component';
import { EmHomeComponent } from './pages/em/em-home/em-home.component';
import { ProfileComponent } from './pages/pm/profile/profile.component';
import { StarsComponent } from './components/stars/stars.component';
import { StarComponent } from './components/stars/star/star.component';
import { ProfileSettingsComponent } from './pages/pm/profile/profile-settings/profile-settings.component';

@NgModule({
  declarations: [
    AppComponent,
    ConfirmComponent,
    MainComponent,
    NavigatorComponent,
    FooterComponent,
    HomeComponent,
    PlanManagerHomeComponent,
    AuthComponent,
    AuthLoginComponent,
    AuthRegistrationComponent,
    AuthPasswordResetComponent,
    LeadZeroPipe,
    PmPlansComponent,
    PmPlanComponent,
    EmHomeComponent,
    AuthPasswordResetComponent,
    ProfileComponent,
    StarsComponent,
    StarComponent,
    ProfileSettingsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    ToastrModule.forRoot(),
    NgbModule,
  ],
  providers: [UtilsService, AuthService, NotificationService, StateService],
  bootstrap: [AppComponent],
  entryComponents: [ConfirmComponent],
})
export class AppModule {}
