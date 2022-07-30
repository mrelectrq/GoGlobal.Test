import { Component, OnDestroy, OnInit } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { Post } from '../../help/interfaces';
import { Subscription } from 'rxjs';
import { AlertService } from '../../services/alert.service';

@Component({
  selector: 'app-cards-page',
  templateUrl: './cards-page.component.html',
  styleUrls: ['./cards-page.component.scss'],
})
export class CardsPageComponent implements OnInit {
  posts = [];
  pSub: Subscription;

  constructor(
    private postsService: PostsService,
    private alert: AlertService
  ) {}

  ngOnInit() {
    this.pSub = this.postsService.getAll().subscribe((posts) => {
      // this.posts = posts.items;
      posts.items.map((item: any) => {
        this.posts.push({
          name: item.name,
          avatar: item.owner.avatar_url,
          description: item.description,
        });
      });
      console.log('this.posts', this.posts);
    });
  }

  ngOnDestroy() {
    if (this.pSub) {
      this.pSub.unsubscribe();
    }
  }
}
