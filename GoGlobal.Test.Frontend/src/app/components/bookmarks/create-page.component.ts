import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IPost } from '../../help/interfaces';
import { PostsService } from '../../services/posts.service';
import { AlertService } from '../../services/alert.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-create-page',
  templateUrl: './bookmarks.component.html',
  styleUrls: ['./bookmarks.component.scss'],
})
export class BookmarksComponent implements OnInit, OnDestroy {
  form: FormGroup;
  wSub: Subscription;
  posts: IPost[] = [];
  errorMessage: boolean;

  constructor(
    private postsService: PostsService,
    private alert: AlertService
  ) {}

  ngOnInit() {
    this.postsService.getAllBookmarks().subscribe((posts) => {
      this.posts = posts;
    });
  }

  deleteBookmark(bookmarkId: string) {
    this.postsService.deleteBookmark(bookmarkId).subscribe(() => {
      this.posts = this.posts.filter(
        (post) => post.repositoryId !== bookmarkId
      );
    });
  }

  ngOnDestroy() {
    if (this.wSub) {
      this.wSub.unsubscribe();
    }
  }
}
