import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { IStory } from '../models/IStory';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { map, tap } from 'rxjs/operators'
import { HttpClient } from '@angular/common/http';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';

@Injectable()
export class StoryResolve implements Resolve<IStory>{
    
    constructor(private http: HttpClient) {}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IStory> {
        const id = route.paramMap.get('id');
        if (id)
        {
            return this.http.get<IStory>(`/api/stories/${id}`);
        }
        
        if (route.url[route.url.length - 1].path == 'add-new'){
            return this.http.post('/api/stories', {})
                .pipe(
                    map(x => <IStory>{id: x, title: 'new story', content: '', seName: '', publicationStatus: 0})
                );
        }
        
        return ErrorObservable.create('error resolving story');
    }
    
}