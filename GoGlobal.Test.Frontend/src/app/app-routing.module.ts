import {NgModule} from '@angular/core';
import {Routes, RouterModule, PreloadAllModules} from '@angular/router';
import { AuthGuard } from './services/auth.guars';

import { LoginPageComponent } from './components/login-page/login-page.component';
import { CreatePageComponent } from './components/create-page/create-page.component';
import { CardsPageComponent } from './components/cards-page/cards-page.component';
import { MainPageComponent } from './components/main-page/main-page.component';


const routes: Routes = [
  {
    path: '', component: MainPageComponent, children: [
      {path: '', component: LoginPageComponent},
      {path: 'cards', component: CardsPageComponent, canActivate: [AuthGuard]},
      {path: 'create', component: CreatePageComponent, canActivate: [AuthGuard]},
    ]
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    preloadingStrategy: PreloadAllModules
  })],
  exports: [RouterModule],
  providers: [AuthGuard]
})
export class AppRoutingModule {
}