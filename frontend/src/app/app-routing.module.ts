import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { ArticleFeed } from './article-feed.component';

const routes: Routes = [

  {
    path: '',
    redirectTo: 'feed',
    pathMatch: 'full'
  },
  {
    path:'feed',
    component : ArticleFeed
  }
  ];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
