import { Component, OnInit } from "@angular/core";
import { StoriesService } from "../stories.service";
import { IStoryHeader } from "../models/IStoryHeader";
import { IStory } from "../models/IStory";
import { Router } from '@angular/router';

@Component({
    selector: 'stories-list',
    templateUrl: './stories-list.component.html',
    styles: [
        `
            table.stories-table {
                cursor: default;
            }
        `
    ]
})
export class StoriesListComponent implements OnInit {

    public stories:IStoryHeader[] = [];

    constructor(
        private storiesService: StoriesService,
        private router: Router
    ) { }

    public ngOnInit(): void {
        this.storiesService.getStoryHeaders(1, 10).subscribe(x => this.stories = x.items);
    }

    public navigateTo(story: IStory) : void {
        this.router.navigate(['stories', story.id]);
    }
}