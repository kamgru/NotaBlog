import { Injectable } from '@angular/core';
import { IStoryHeader } from './models/IStoryHeader';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { IStory } from './models/IStory';

@Injectable()
export class StoriesService {

    public getStoryHeaders(): Observable<IStoryHeader[]> {
        return of([]);
    }

    public getStory(id:string): Observable<IStory> {
        return of({id:id, title: '', content: '', publicationStatus: 1, seName: ''});
    }
}