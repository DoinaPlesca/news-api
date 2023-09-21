import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { firstValueFrom } from 'rxjs';
import { Article, ResponseDto } from 'src/models';
import { ModalController, ToastController } from '@ionic/angular';
import { State } from '../state';

@Component({
  template: `
    <ion-list>
      <ion-card *ngFor="let article of state.test">
        <ion-toolbar>
          <ion-title color="secondary">{{ article.headline }}</ion-title>
          <ion-card-subtitle>{{ article.body }}</ion-card-subtitle>
          <img style="max-height: 200px;" [src]="article.articleImgUrl" />
        </ion-toolbar>
      </ion-card>
    </ion-list>
  `,
})


export class ArticleFeed implements OnInit {
  constructor(
    public http: HttpClient,
    public modalController: ModalController,
    public state: State,
    public toastController: ToastController
  ) {}

  async fetchArticles() {
    try {
      const result = await firstValueFrom(
        this.http.get<ResponseDto<Article[]>>(environment.baseUrl + '/api/feed')
      );

      this.state.test = result.responseData || [];

      const toast = await this.toastController.create({
        message: result.messageToClient,
        duration: 2000,
      });
      toast.present();
    } catch (error) {
      const toast = await this.toastController.create({
        message:
          'Error fetching articles. Please try again later. Check if API is still running',
        duration: 2000,
      });
      toast.present();
    }
  }

  ngOnInit(): void {
    this.fetchArticles();
  }
}
