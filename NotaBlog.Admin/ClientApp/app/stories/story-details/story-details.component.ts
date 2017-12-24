import { Component, OnInit } from '@angular/core';
import { IStory } from '../models/IStory';
import { StoriesService } from '../stories.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'story-details',
    templateUrl: './story-details.component.html'
})
export class StoryDetailsComponent implements OnInit {

    public story:IStory;

    constructor(
        private storiesService: StoriesService,
        private route: ActivatedRoute
    ){}

    public ngOnInit(): void {
        const id = this.route.snapshot.paramMap.get('id') || ''; 
        this.storiesService.getStory(id).subscribe(x => this.story = x);
    }
}