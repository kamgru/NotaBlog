import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import { IStory } from '../models/IStory';
import { StoriesService } from '../stories.service';
import { catchError, tap } from 'rxjs/operators';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';

@Component({
    selector: 'story-details',
    templateUrl: './story-details.component.html'
})
export class StoryDetailsComponent implements OnInit {

    private story:IStory;
    private error:string | null;
    private showSuccess:boolean = false;

    public storyForm = new FormGroup({
        title: new FormControl(),
        content: new FormControl()
    })

    constructor(
        private storiesService: StoriesService,
        private route: ActivatedRoute,
    ){}

    public ngOnInit(): void {
        this.story = this.route.snapshot.data['story'];
        this.storyForm.reset({
            title: this.story.title,
            content: this.story.content 
        });
        this.storyForm.valueChanges.subscribe(x => {
        });
    }

    public onSubmit(): void {
        this.storiesService.updateStory(this.story.id, this.storyForm.value.title, this.storyForm.value.content)
            .pipe(
                tap(this.handleSuccess),
                catchError(this.handleError),
            )
            .subscribe()
    }

    handleSuccess(): void {
        this.showSuccess = true;
        this.error = null;
        this.storyForm.markAsPristine();
    }

    handleError(httpError: any): any {
        this.error = httpError.error;
        this.showSuccess = false;
        return ErrorObservable.create('update error');
    }
}