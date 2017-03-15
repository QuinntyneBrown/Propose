import { fetch } from "../utilities";
import { Ideation } from "./ideation.model";

export class IdeationService {
    constructor(private _fetch = fetch) { }

    private static _instance: IdeationService;

    public static get Instance() {
        this._instance = this._instance || new IdeationService();
        return this._instance;
    }

    public get(): Promise<Array<Ideation>> {
        return this._fetch({ url: "/api/ideation/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { ideations: Array<Ideation> }).ideations;
        });
    }

    public getById(id): Promise<Ideation> {
        return this._fetch({ url: `/api/ideation/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { ideation: Ideation }).ideation;
        });
    }

    public add(ideation) {
        return this._fetch({ url: `/api/ideation/add`, method: "POST", data: { ideation }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/ideation/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
