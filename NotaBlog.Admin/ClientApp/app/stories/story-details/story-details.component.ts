import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import { IStory } from '../models/IStory';
import { StoriesService } from '../stories.service';

@Component({
    selector: 'story-details',
    templateUrl: './story-details.component.html'
})
export class StoryDetailsComponent implements OnInit {

    private story:IStory;

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
            console.log(x)
        });
    }

    public onSubmit(): void {
        console.log(this.storyForm.value)
    }
}