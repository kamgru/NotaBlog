import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoriesService } from './stories.service';
import { StoriesListComponent } from './stories-list.component';
import { StoriesRoutingModule } from './stories-routing.module';
import { StoryDetailsComponent } from './story-details.component';

@NgModule({
    imports: [CommonModule, StoriesRoutingModule],
    declarations: [StoriesListComponent, StoryDetailsComponent],
    providers: [StoriesService],
    exports: [StoriesRoutingModule]
})
export class StoriesModule {}