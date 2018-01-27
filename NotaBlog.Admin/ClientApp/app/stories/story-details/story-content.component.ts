import { Component, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { IStory } from '../models/IStory';

@Component({
    selector: 'story-content',
    templateUrl: './story-content.component.html'
})
export class StoryContentComponent implements OnChanges {

    @Input() story:IStory;
    @Output() onStoryUpdate = new EventEmitter();
    
    public storyForm = new FormGroup({
        title: new FormControl(),
        content: new FormControl()
    });

    public ngOnChanges(changes: SimpleChanges): void {
        if (changes.story) {
            this.storyForm.reset({
                title: this.story.title,
                content: this.story.content
            });
        }
    }

    public onSubmit():void {
        const value = this.storyForm.value;
        this.onStoryUpdate.emit(value);
    }
}