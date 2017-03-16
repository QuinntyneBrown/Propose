import { fetch } from "../utilities";
import { TeamLeader } from "./team-leader.model";

export class TeamLeaderService {
    constructor(private _fetch = fetch) { }

    private static _instance: TeamLeaderService;

    public static get Instance() {
        this._instance = this._instance || new TeamLeaderService();
        return this._instance;
    }

    public get(): Promise<Array<TeamLeader>> {
        return this._fetch({ url: "/api/teamleader/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { teamLeaders: Array<TeamLeader> }).teamLeaders;
        });
    }

    public getById(id): Promise<TeamLeader> {
        return this._fetch({ url: `/api/teamleader/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { teamLeader: TeamLeader }).teamLeader;
        });
    }

    public add(teamLeader) {
        return this._fetch({ url: `/api/teamleader/add`, method: "POST", data: { teamLeader }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/teamleader/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
