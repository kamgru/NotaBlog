import { Component, Input, Output, EventEmitter, SimpleChanges, OnChanges } from '@angular/core';
import { IStory } from '../models/IStory';

@Component({
    selector: 'publication-status',
    templateUrl: './publication-status.component.html'
})
export class PublicationStatusComponent implements OnChanges {
    @Input() story:IStory;
    @Output() onPublish = new EventEmitter();
    @Output() onUnpublish = new EventEmitter();

    public ngOnChanges(changes:SimpleChanges):void {
        if(changes.story) {
            this.story = changes.story.currentValue;
        }
    }

    private publish():void{
        this.onPublish.emit();
    }

    private unpublish():void{
        this.onUnpublish.emit();
    }
}