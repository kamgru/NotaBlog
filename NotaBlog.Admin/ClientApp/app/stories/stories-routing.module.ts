import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StoriesListComponent } from './stories-list/stories-list.component';
import { StoryDetailsComponent } from './story-details/story-details.component';
import { StoryResolve } from './story-details/story.resolve';
import { StoriesComponent } from './stories.component';

const storiesRoutes:Routes = [
    {path: 'stories', component: StoriesComponent, children: [
        {path: '', component: StoriesListComponent},
        {path: 'add-new', component: StoryDetailsComponent, resolve: {story: StoryResolve} },
        {path: ':id', component: StoryDetailsComponent, resolve: {story: StoryResolve}}
    ]}
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