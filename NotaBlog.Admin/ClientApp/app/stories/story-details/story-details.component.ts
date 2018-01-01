import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { IStory } from '../models/IStory';
import { StoriesService } from '../stories.service';
import { catchError, tap, map } from 'rxjs/operators';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { EmptyObservable } from 'rxjs/observable/EmptyObservable';
import { IApiResult } from '../../models/IApiResult';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'story-details',
    templateUrl: './story-details.component.html'
})
export class StoryDetailsComponent implements OnInit {

    private story:IStory;
    private error:string | null;
    private success:string | null;

    private handleApiError = catchError(err => {
        if (err instanceof HttpErrorResponse){
            this.error = err.error;
            this.success = null;
        }
        return new EmptyObservable();
    })

    private handleApiSuccess(message:string):void {
        this.success = message;
        this.error = null;
    }

    constructor(
        private storiesService: StoriesService,
        private route: ActivatedRoute,
    ){}

    public ngOnInit(): void {
        this.story = this.route.snapshot.data['story'];
    }

    private updateContent(story:IStory):void {
        this.storiesService.updateStory(this.story.id, story.title, story.content)
            .pipe(
                tap(_ => this.handleApiSuccess('update succesful')),
                this.handleApiError
            )
            .subscribe();
    }

    private publishStory(): void {
        this.storiesService.updateStatus(this.story.id, 1)
        .pipe(
            tap(_ => {this.handleApiSuccess('publication succesful'); this.story.publicationStatus = 1}),
            this.handleApiError
        )
        .subscribe()
    }

    private unpublishStory(): void {
        this.storiesService.updateStatus(this.story.id, 0)
        .pipe(
            tap(_ => {this.handleApiSuccess('publication succesful'); this.story.publicationStatus = 0}),
            this.handleApiError
        )
        .subscribe()
    }

    private updateSeName(seName:string):void{
        this.storiesService.updateSeName(this.story.id, seName)
        .pipe(
            tap(_ => {this.handleApiSuccess('update successful'); this.story.seName = seName;}),
            this.handleApiError
        )
        .subscribe();
    }
}