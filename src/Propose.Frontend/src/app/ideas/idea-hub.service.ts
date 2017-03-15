import { BehaviorSubject } from "rxjs"

export class IdeaHub extends BehaviorSubject<any>  {
    constructor() {  
        super(null);
        this.votedIdea = this.votedIdea.bind(this);
        this.ideaAddedOrUpdated = this.ideaAddedOrUpdated.bind(this);
        this.next = this.next.bind(this);
                      
        var connection = ($ as any).hubConnection();
        this._hub = connection.createHubProxy("ideaHub");

        this._hub.on("votedIdea", this.next);
        this._hub.on("ideaAddedOrUpdated", this.next);

        connection.start();
    }

    private static _instance: IdeaHub;

    public static get Instance() {
        this._instance = this._instance || new IdeaHub();
        return this._instance;
    }

    public votedIdea(options) { this._hub.invoke('votedIdea',options); }

    public ideaAddedOrUpdated(options) { this._hub.invoke('ideaAddedOrUpdated', options); }

    private _hub: any;
}

