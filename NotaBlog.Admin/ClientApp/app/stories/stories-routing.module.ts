import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StoriesListComponent } from './stories-list/stories-list.component';
import { StoryDetailsComponent } from './story-details/story-details.component';

const storiesRoutes:Routes = [
    {path: 'stories', component: StoriesListComponent},
    {path: 'stories/:id', component: StoryDetailsComponent}
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