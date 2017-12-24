import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoriesService } from './stories.service';
import { StoriesListComponent } from './stories-list/stories-list.component';
import { StoriesRoutingModule } from './stories-routing.module';
import { StoryDetailsComponent } from './story-details/story-details.component';
import { AppModuleShared } from '../app.shared.module';
import { PublicationStatusPipe } from './publication-status.pipe';

@NgModule({
    imports: [CommonModule, StoriesRoutingModule, AppModuleShared],
    declarations: [StoriesListComponent, StoryDetailsComponent, PublicationStatusPipe],
    providers: [StoriesService],
    exports: [StoriesRoutingModule]
})
export class StoriesModule {}