import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoriesService } from './stories.service';
import { StoriesListComponent } from './stories-list.component';
import { StoriesRoutingModule } from './stories-routing.module';
import { StoryDetailsComponent } from './story-details.component';
import { AppModuleShared } from '../app.shared.module';

@NgModule({
    imports: [CommonModule, StoriesRoutingModule, AppModuleShared],
    declarations: [StoriesListComponent, StoryDetailsComponent],
    providers: [StoriesService],
    exports: [StoriesRoutingModule]
})
export class StoriesModule {}