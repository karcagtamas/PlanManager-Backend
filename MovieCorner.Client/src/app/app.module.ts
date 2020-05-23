import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MaterialModule } from './material/material.module';
import { AppRoutingModule } from './app-routing.module';
import {
  BrowserAnimationsModule,
  NoopAnimationsModule
} from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import {
  LoaderService,
  NotificationService,
  UserService,
  UtilService,
  CommonService
} from './services';
import { UserComponent } from './pages/user/user.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { NavigatorComponent } from './layout/navigator/navigator.component';
import { ProfileComponent } from './pages/home/profile/profile.component';
import { MainComponent } from './pages/home/main/main.component';
import { TodosComponent } from './pages/home/todos/todos.component';
import { WorkingManagerComponent } from './pages/home/working-manager/working-manager.component';
import { UserDetailsComponent } from './components/profile/user-details/user-details.component';
import { UserModifyComponent } from './components/profile/user-modify/user-modify.component';
import { SidenavComponent } from './layout/sidenav/sidenav.component';
import { UserNotificationsComponent } from './components/profile/user-notifications/user-notifications.component';
import { UserMessagesComponent } from './components/profile/user-messages/user-messages.component';
import { MoviesComponent } from './pages/home/movies/movies.component';
import { SeriesComponent } from './pages/home/series/series.component';
import { MoviesAllComponent } from './components/movies/movies-all/movies-all.component';
import { MoviesMyComponent } from './components/movies/movies-my/movies-my.component';
import { SeriesMyComponent } from './components/series/series-my/series-my.component';
import { SeriesAllComponent } from './components/series/series-all/series-all.component';
import { MoviesModalComponent } from './components/movies/movies-modal/movies-modal.component';
import { DescriptionPipe } from './pipes/description.pipe';
import { ConfirmModalComponent } from './components/common/confirm-modal/confirm-modal.component';
import { MoviesMapperModalComponent } from './components/movies/movies-mapper-modal/movies-mapper-modal.component';
import { SeenPipe } from './pipes/seen.pipe';
import { LoaderInterceptor } from './interceptors/loader.interceptor';
import { LoaderComponent } from './components/loader/loader.component';
import { UnSolvedTodosComponent } from './components/todos/un-solved-todos/un-solved-todos.component';
import { SolvedTodosComponent } from './components/todos/solved-todos/solved-todos.component';
import { WorkingFieldsComponent } from './components/working-manager/working-fields/working-fields.component';
import { LeadZeroPipe } from './pipes/lead-zero.pipe';
import { WorkingDayModalComponent } from './components/working-manager/working-day-modal/working-day-modal.component';
import { WorkingFieldModalComponent } from './components/working-manager/working-field-modal/working-field-modal.component';
import { SeasonTableComponent } from './components/series/season-table/season-table.component';
import { EpisodeTableComponent } from './components/series/episode-table/episode-table.component';
import { SeriesModalComponent } from './components/series/series-modal/series-modal.component';
import { TodoModalComponent } from './components/todos/todo-modal/todo-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    HomeComponent,
    LoginComponent,
    RegistrationComponent,
    NavigatorComponent,
    ProfileComponent,
    MainComponent,
    TodosComponent,
    WorkingManagerComponent,
    UserDetailsComponent,
    UserModifyComponent,
    SidenavComponent,
    UserNotificationsComponent,
    UserMessagesComponent,
    MoviesComponent,
    SeriesComponent,
    MoviesAllComponent,
    MoviesMyComponent,
    SeriesMyComponent,
    SeriesAllComponent,
    MoviesModalComponent,
    DescriptionPipe,
    ConfirmModalComponent,
    MoviesMapperModalComponent,
    SeenPipe,
    LoaderComponent,
    UnSolvedTodosComponent,
    SolvedTodosComponent,
    WorkingFieldsComponent,
    LeadZeroPipe,
    WorkingDayModalComponent,
    WorkingFieldModalComponent,
    SeasonTableComponent,
    EpisodeTableComponent,
    SeriesModalComponent,
    TodoModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NoopAnimationsModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot({ progressBar: true })
  ],
  providers: [
    CommonService,
    LoaderService,
    NotificationService,
    UserService,
    UtilService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true }
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    MoviesModalComponent,
    ConfirmModalComponent,
    MoviesMapperModalComponent,
    WorkingFieldModalComponent,
    WorkingDayModalComponent,
    SeriesModalComponent,
    TodoModalComponent
  ]
})
export class AppModule {}
