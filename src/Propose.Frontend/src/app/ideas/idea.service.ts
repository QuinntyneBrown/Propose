import { fetch } from "../utilities";
import { Idea } from "./idea.model";

export class IdeaService {
    constructor(private _fetch = fetch) { }

    private static _instance: IdeaService;

    public static get Instance() {
        this._instance = this._instance || new IdeaService();
        return this._instance;
    }

    public get(): Promise<Array<Idea>> {
        return this._fetch({ url: "/api/idea/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { ideas: Array<Idea> }).ideas;
        });
    }

    public getById(id): Promise<Idea> {
        return this._fetch({ url: `/api/idea/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { idea: Idea }).idea;
        });
    }

    public add(idea) {
        return this._fetch({ url: `/api/idea/add`, method: "POST", data: { idea }, authRequired: true  });
    }

    public vote(options: { id: number }) {
        return this._fetch({ url: `/api/idea/vote?id=${options.id}`, method: "POST", authRequired: true });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/idea/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
