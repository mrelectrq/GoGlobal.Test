import {Component, OnInit, OnDestroy} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Post} from '../../help/interfaces';
import {PostsService} from '../../services/posts.service';
import {AlertService} from '../../services/alert.service';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-create-page',
  templateUrl: './create-page.component.html',
  styleUrls: ['./create-page.component.scss']
})
export class CreatePageComponent implements OnInit, OnDestroy {

  form: FormGroup;
  wSub: Subscription;
  card
  errorMessage: boolean

  constructor(
    private postsService: PostsService,
    private alert: AlertService
  ) {
  }

  ngOnInit() {
    this.form = new FormGroup({
      title: new FormControl(null, Validators.required),
      city: new FormControl(null, Validators.required),
      description: new FormControl(null, Validators.required),
      tags: new FormControl(null, Validators.required)
    })
   this.errorMessage = false;

  }

  submit() {
    let post: Post
    if (this.form.invalid) {
      return
    }
    this.wSub = this.postsService.getWeatherMap(this.form.value.city).subscribe(posts => {
      this.card = posts
    }, err => { 
      console.error(err); 
      this.errorMessage = true;
    });
    setTimeout(() => {
      if(this.card){
      post = {
        title: this.form.value.title,
        city: this.form.value.city,
        description: this.form.value.description,
        tags: this.form.value.tags,
        date: new Date(),
        weather: {
          icon: this.card?.weather[0].icon,
          feels_like: this.card?.main?.feels_like,
          temp: this.card.main.temp,
          description: this.card.weather[0].main
        }
      }
     
        this.postsService.create(post).subscribe(() => {
          this.form.reset()
          this.alert.success('Сard was created');
          this.errorMessage = false;
        })
      }      
    }, 1000);   
  }




  ngOnDestroy() {
    if (this.wSub) {
      this.wSub.unsubscribe()
    }

   
  }

}
