import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoriesRoutingModule } from './stories-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from '../shared/shared.module';

import { StoriesService } from './stories.service';
import { StoryResolve } from './story-details/story.resolve';

import { StoriesComponent } from './stories.component';
import { StoriesListComponent } from './stories-list/stories-list.component';
import { StoryDetailsComponent } from './story-details/story-details.component';
import { PublicationStatusPipe } from './publication-status.pipe';

@NgModule({
    imports: [
        CommonModule, 
        SharedModule,
        ReactiveFormsModule,
        HttpClientModule,
        StoriesRoutingModule, 
    ],
    declarations: [
        StoriesComponent,
        StoriesListComponent, 
        StoryDetailsComponent, 
        PublicationStatusPipe
    ],
    providers: [
        StoriesService, 
        StoryResolve
    ],
    exports: [
        StoriesRoutingModule
    ]
})
export class StoriesModule {}