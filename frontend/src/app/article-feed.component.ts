import { Component, OnInit } from "@angular/core";
import {HttpClient} from "@angular/common/http";
import { environment } from "src/environments/environment";
import { firstValueFrom } from "rxjs";
import { Article, ResponseDto } from "src/models";
import { State } from "src/state";
import { ModalController, ToastController } from "@ionic/angular";


@Component ({

  template :
    `
     <ion-list>
       <ion-card  *ngFor ="let article of state.articles">
         <ion-toolbar>
           <ion-title>{{article.headline}}</ion-title>
         </ion-toolbar>
       </ion-card>
     </ion-list>


  `
})

export class ArticleFeed implements OnInit {
  constructor(public http: HttpClient,
              public modalController: ModalController,
              public state: State,
              public toastController: ToastController) {
  }

  async fetchArticles() {
    const result = await firstValueFrom(this.http.get<ResponseDto<Article[]>>(environment.baseUrl + '/api/feed'))
    this.state.articles = result.responseDate!;


  }

  ngOnInit(): void {
    this.fetchArticles()
  }


}
