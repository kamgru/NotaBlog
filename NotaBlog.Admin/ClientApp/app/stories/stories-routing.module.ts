import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StoriesListComponent } from './stories-list/stories-list.component';
import { StoryDetailsComponent } from './story-details/story-details.component';
import { StoryResolve } from './story-details/story.resolve';

const storiesRoutes:Routes = [
    {path: 'stories', component: StoriesListComponent},
    {path: 'stories/add-new', component: StoryDetailsComponent, resolve: {story: StoryResolve} },
    {path: 'stories/:id', component: StoryDetailsComponent, resolve: {story: StoryResolve}}
];

@NgModule({
    imports: [
        RouterModule.forChild(storiesRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class StoriesRoutingModule { }