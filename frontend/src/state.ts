import {Injectable} from "@angular/core";
import { Article } from "src/models";

@Injectable({
  providedIn: 'root'
})

export class State {
  test:Article[] = [];
}


