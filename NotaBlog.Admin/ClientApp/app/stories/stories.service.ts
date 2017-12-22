import { Injectable } from '@angular/core';
import { IStoryHeader } from './models/IStoryHeader';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { IStory } from './models/IStory';

const data:IStoryHeader[] = [
    {id:"1", title: "fake1"},
    {id:"2", title: "fake2"},
    {id:"3", title: "fake3"},
    {id:"4", title: "fake4"},
    {id:"5", title: "fake5"},
];

@Injectable()
export class StoriesService {

    public getStoryHeaders(): Observable<IStoryHeader[]> {
        return of(data);
    }

    public getStory(id:string): Observable<IStory> {
        return of({id:id, title: '', content: '', publicationStatus: 1, seName: ''});
    }
}