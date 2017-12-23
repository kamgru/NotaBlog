import { Component, OnInit } from "@angular/core";
import { StoriesService } from "./stories.service";
import { IStoryHeader } from "./models/IStoryHeader";


@Component({
    selector: 'stories-list',
    templateUrl: './stories-list.component.html'
})
export class StoriesListComponent implements OnInit {

    public stories:IStoryHeader[] = [];

    constructor(private storiesService: StoriesService) { }

    public ngOnInit(): void {
        this.storiesService.getStoryHeaders(1, 10).subscribe(x => this.stories = x.items);
    }

}