import { Component, OnDestroy, OnInit } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { IPost } from '../../help/interfaces';
import { Subscription } from 'rxjs';
import { AlertService } from '../../services/alert.service';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-cards-page-page',
  templateUrl: './cards-page.component.html',
  styleUrls: ['./cards-page.component.scss'],
})
export class CardsPageComponent implements OnInit {
  posts: IPost[] = [];
  pSub: Subscription;
  searchControl: FormControl = new FormControl();

  constructor(
    private postsService: PostsService,
    private alert: AlertService
  ) {}

  ngOnInit() {
    //  if (!this.posts.length) {
    //   console.log(this.searchControl.value)
    //   this.pSub = this.postsService.getAll(this.searchControl.value).subscribe((posts) => {
    //     // this.posts = posts.items;
    //     posts.items.map((item: any) => {
    //       this.posts.push({
    //         repositoryName: item.full_name,
    //         avatar: item.owner.avatar_url,
    //         repositoryDescription: item.description,
    //       });
    //     });
    //     console.log('this.posts', this.posts);
    //   });
    //  }
  }

  search() {
    this.pSub = this.postsService
      .searchForRepositories(this.searchControl.value)
      
      .subscribe((posts) => {
        console.log('this.posts', posts);
        this.posts = [];
        posts.map((item) => {
          this.posts.push({
            repositoryName: item.repositoryName,
            avatar: item.avatar,
            repositoryDescription: item.repositoryDescription,
          });
        });
      });
  }

  saveBookmark(newBookmark: IPost) {
    this.postsService.addBookmark(newBookmark).subscribe(() => {
      this.posts = this.posts.filter(
        (post) => post.repositoryName !== newBookmark.repositoryName
      );
    });
  }

  ngOnDestroy() {
    if (this.pSub) {
      this.pSub.unsubscribe();
    }
  }
}
