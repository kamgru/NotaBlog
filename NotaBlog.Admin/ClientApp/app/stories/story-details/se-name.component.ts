import { Component, Input, Output, OnChanges, SimpleChanges, EventEmitter } from '@angular/core';
import { IStory } from '../models/IStory';
import { FormControl } from '@angular/forms';

@Component({
    selector: 'se-name',
    templateUrl: './se-name.component.html'
})
export class SeNameComponent implements OnChanges {

    @Input() story:IStory;
    @Output() onSeNameChange = new EventEmitter();

    seNameForm = new FormControl('');

    public ngOnChanges(changes: SimpleChanges): void {
        if (changes.story){
            this.story = changes.story.currentValue;
            this.seNameForm.reset(this.story.seName || '');
        }    
    }

    public save(): void {
        this.onSeNameChange.emit(this.seNameForm.value);
    }
}