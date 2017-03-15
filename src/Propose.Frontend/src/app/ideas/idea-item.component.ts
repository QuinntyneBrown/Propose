import { Idea } from "./idea.model";
import { IdeaHub } from "./idea-hub.service";
import { IdeaService } from "./idea.service";
import { IdeaEventType } from "./idea-event-type";
import { EditorComponent } from "../shared";
import { Router } from "../router";
import { CurrentUser } from "../users";
import { pluckOut } from "../utilities";

const template = require("./idea-item.component.html");
const styles = require("./idea-item.component.scss");

export class IdeaItemComponent extends HTMLElement {
    constructor(
        private _ideaHub: IdeaHub = IdeaHub.Instance,
        private _ideaService: IdeaService = IdeaService.Instance,
        private _router: Router = Router.Instance,
        private _currentUser: CurrentUser = CurrentUser.Instance
    ) {
        super();

        this._onDeleteClick = this._onDeleteClick.bind(this);
        this._onEditClick = this._onEditClick.bind(this);
        this._onViewClick = this._onViewClick.bind(this);
        this._onClick = this._onClick.bind(this);
        this._addOrRemove = this._addOrRemove.bind(this);
        this._onOtherVotedIdea = this._onOtherVotedIdea.bind(this);

        _ideaHub.subscribe(this._onOtherVotedIdea);
    }

    static get observedAttributes() {
        return ["entity"];
    }

    public _onOtherVotedIdea(options: { userId: any, ideaId: any, eventType: IdeaEventType }) {        
        if (options && options.eventType == IdeaEventType.VotedIdea && this.entity.id == options.ideaId) {
            this._addOrRemove({ votes: this.entity.votes, userId: options.userId });
            this._nameElement.textContent = `${this.entity.name} - ${this.entity.votes.length}`;
        }        
    }

    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    disconnectedCallback() {
        this._deleteLinkElement.removeEventListener("click", this._onDeleteClick);
        this._editLinkElement.removeEventListener("click", this._onEditClick);
        this._viewLinkElement.removeEventListener("click", this._onViewClick);
        this.removeEventListener("click", this._onClick);
    }

    private _bind() {
        this._nameElement.textContent = `${this.entity.name} - ${this.entity.votes.length}`;
        this._htmlBodyElement.innerHTML = this.entity.htmlBody;
    }

    private _setEventListeners() {
        this._deleteLinkElement.addEventListener("click", this._onDeleteClick);
        this._editLinkElement.addEventListener("click", this._onEditClick);
        this._viewLinkElement.addEventListener("click", this._onViewClick);
        this.addEventListener("click", this._onClick);
    }

    private async _onDeleteClick(e:Event) {
        await this._ideaService.remove({ id: this.entity.id });        
        this.parentNode.removeChild(this);
    }

    private _onEditClick() {
        this._router.navigate(["idea", "edit", this.entity.id]);
    }

    private _onViewClick() {
        this._router.navigate(["idea","view",this.entity.id]);
    }

    private _addOrRemove(options:{ votes:Array<any>, userId:any }) {
        var existingVote = options.votes.find((x) => { return x.userId == options.userId });

        if (existingVote) {
            pluckOut({ key: "userId", value: existingVote.userId, items: options.votes });
        } else {
            options.votes.push({ userId: options.userId });
        }
    }

    private async _onClick() {                
        this._addOrRemove({ votes: this.entity.votes, userId: this._currentUser.userId });

        this._nameElement.textContent = `${this.entity.name} - ${this.entity.votes.length}`;

        await this._ideaService.vote({ id: this.entity.id });        

        this._ideaHub.votedIdea({
            userId: this._currentUser.userId,
            ideaId: this.entity.id
        });
    }
    
    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "entity":
                this.entity = JSON.parse(newValue);
                break;
        }        
    }

    private get _nameElement() { return this.querySelector("p") as HTMLElement; }
    private get _deleteLinkElement() { return this.querySelector(".entity-item-delete") as HTMLElement; }
    private get _editLinkElement() { return this.querySelector(".entity-item-edit") as HTMLElement; }
    private get _viewLinkElement() { return this.querySelector(".entity-item-view") as HTMLElement; }
    private get _htmlBodyElement() { return this.querySelector(".idea-html-body") as HTMLElement; }

    public entity: Idea;
}

customElements.define(`ce-idea-item`,IdeaItemComponent);
