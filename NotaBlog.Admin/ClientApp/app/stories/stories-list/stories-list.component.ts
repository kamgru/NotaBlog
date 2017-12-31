import { Component, OnInit } from "@angular/core";
import { StoriesService } from "../stories.service";
import { IStoryHeader } from "../models/IStoryHeader";
import { IStory } from "../models/IStory";
import { Router } from '@angular/router';
import { IPaginatedData } from "../../models/IPaginatedData";

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

    private data:IPaginatedData<IStoryHeader> = {items: [], totalCount: 0};
    pageSize = 10;

    constructor(
        private storiesService: StoriesService,
        private router: Router
    ) { }

    private pageSizeChanged($event:any):void {
        const opts = $event.target.selectedOptions[0];
        this.pageSize = opts.value;
        this.loadData(1);
    }

    public ngOnInit(): void {
        this.loadData(1);
    }

    public navigateTo(story: IStory) : void {
        this.router.navigate(['stories', story.id]);
    }
    public pageChange(page:any) {
        this.loadData(page);
    }

    private loadData(page:number):void{
        this.storiesService.getStoryHeaders(page, this.pageSize)
        .subscribe(data => {
            this.data = data; 
        });
    }
}