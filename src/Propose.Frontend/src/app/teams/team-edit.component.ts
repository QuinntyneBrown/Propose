import { Team } from "./team.model";
import { TeamService } from "./team.service";
import { EditorComponent } from "../shared";
import { Router } from "../router";

const template = require("./team-edit.component.html");
const styles = require("./team-edit.component.scss");

export class TeamEditComponent extends HTMLElement {
    constructor(
        private _teamService: TeamService = TeamService.Instance,
        private _router: Router = Router.Instance
        ) {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onTitleClick = this.onTitleClick.bind(this);
    }

    static get observedAttributes() {
        return ["team-id"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.teamId ? "Edit Team": "Create Team";

        if (this.teamId) {
            const team: Team = await this._teamService.getById(this.teamId);                
            this._nameInputElement.value = team.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._titleElement.addEventListener("click", this.onTitleClick);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._titleElement.removeEventListener("click", this.onTitleClick);
    }

    public async onSave() {
        const team = {
            id: this.teamId,
            name: this._nameInputElement.value
        } as Team;
        
        await this._teamService.add(team);
        this._router.navigate(["team","list"]);
    }

    public async onDelete() {        
        await this._teamService.remove({ id: this.teamId });
        this._router.navigate(["team","list"]);
    }

    public onTitleClick() {
        this._router.navigate(["team", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "team-id":
                this.teamId = newValue;
                break;
        }        
    }

    public teamId: number;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".team-name") as HTMLInputElement;}
}

customElements.define(`ce-team-edit`,TeamEditComponent);
