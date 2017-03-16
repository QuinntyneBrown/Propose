import { Idea } from "./idea.model";
import { IdeaService } from "./idea.service";
import { IdeaHub } from "./idea-hub.service";
import { EditorComponent, tabsEvents } from "../shared";
import { Router } from "../router";
import { IdeationService, Ideation } from "../ideations";
import { digitalAssetActions, DigitalAsset } from "../digital-assets";
import { linkActions, Link, } from "../links";


const template = require("./idea-edit.component.html");
const styles = require("./idea-edit.component.scss");

export class IdeaEditComponent extends HTMLElement {
    constructor(
        private _ideaService: IdeaService = IdeaService.Instance,
        private _ideaHub: IdeaHub = IdeaHub.Instance,
        private _ideationService: IdeationService = IdeationService.Instance,
        private _router: Router = Router.Instance
        ) {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onTitleClick = this.onTitleClick.bind(this);

        this.onDigitalAssetSave = this.onDigitalAssetSave.bind(this);
        this.onDigitalAssetDelete = this.onDigitalAssetDelete.bind(this);
        this.onDigitalAssetEdit = this.onDigitalAssetEdit.bind(this);

        this.onLinkSave = this.onLinkSave.bind(this);
        this.onLinkDelete = this.onLinkDelete.bind(this);
        this.onLinkEdit = this.onLinkEdit.bind(this);


        this.onTabSelectedIndexChanged = this.onTabSelectedIndexChanged.bind(this);
    }

    static get observedAttributes() {
        return ["idea-id", "tab-index"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this._titleElement.textContent = this.ideaId ? "Edit Idea" : "Create Idea";

        this.tabsElement.setAttribute("custom-tab-index", `${this.customTabIndex}`);

        this.htmlBodyEditor = new EditorComponent(this._htmlBodyInputElement);

        this.htmlDescriptionEditor = new EditorComponent(this._htmlDescriptionInputElement);

        let promises: Array<Promise<any>> = [this._ideationService.get()];

        if (this.ideaId)
            promises.push(this._ideaService.getById(this.ideaId));

        const results = await Promise.all(promises);

        const ideations = results[0] as Array<Ideation>;

        for (let i = 0; i < ideations.length; i++) {
            let option = document.createElement("option");
            option.textContent = ideations[i].name;
            option.value = ideations[i].id;
            this._ideationSelectElement.appendChild(option);
        }

        if (this.ideaId) {
            const idea: Idea = results[1];
            this._nameInputElement.value = idea.name;
            this.htmlBodyEditor.setHTML(idea.htmlBody);
            this.htmlDescriptionEditor.setHTML(idea.htmlDescription);
            this.digitalAssets = idea.digitalAssets;
            this.links = idea.links;

            this.linkListElement.setAttribute("links", JSON.stringify(this.links));
            this.digitalAssetListElement.setAttribute("digital-assets", JSON.stringify(this.digitalAssets));

        } else {
            this._deleteButtonElement.style.display = "none";
        }
    }

    public onLinkSave(e) {        
        const index = this.links.findIndex(o => o.id == e.detail.link.id);
        const indexBasedOnName = this.links.findIndex(o => o.name == e.detail.link.name);

        if (index > -1 && e.detail.link.id != null) {
            this.links[index] = e.detail.link;
        } else if (indexBasedOnName > -1) {
            for (var i = 0; i < this.links.length; ++i) {
                if (this.links[i].name == e.detail.link.name)
                    this.links[i] = e.detail.link;
            }
        } else {
            this.links.push(e.detail.link);
        }


        this.linkEditorElement.setAttribute("link", JSON.stringify(new Link()));
        this.linkListElement.setAttribute("links", JSON.stringify(this.links));
    }

    public onLinkEdit(e) {
        this.linkEditorElement.setAttribute("link", JSON.stringify(e.detail.link));
    }

    public onLinkDelete(e) {
        if (e.detail.linkId != null && e.detail.linkId != undefined) {
            this.linkEditorElement.setAttribute("link", JSON.stringify(new Link()));
            this.links.splice(this.links.findIndex(o => o.id == e.detail.linkId), 1);
            this.linkListElement.setAttribute("links", JSON.stringify(this.links));
        } else {
            for (var i = 0; i < this.links.length; ++i) {
                if (this.links[i].name == e.detail.linkName)
                    this.links.splice(i, 1);
            }

            this.linkEditorElement.setAttribute("link", JSON.stringify(new Link()));
            this.linkListElement.setAttribute("links", JSON.stringify(this.links));
        }
    }

    public onDigitalAssetSave(e) {
        
        const index = this.digitalAssets.findIndex(o => o.id == e.detail.digitalAsset.id);
        const indexBasedOnName = this.digitalAssets.findIndex(o => o.digitalAssetUrl == e.detail.digitalAsset.digitalAssetUrl);

        if (index > -1 && e.detail.digitalAsset.id != null) {
            this.digitalAssets[index] = e.detail.digitalAsset;
        } else if (indexBasedOnName > -1) {
            for (var i = 0; i < this.digitalAssets.length; ++i) {
                if (this.digitalAssets[i].digitalAssetUrl == e.detail.digitalAsset.digitalAssetUrl)
                    this.digitalAssets[i] = e.detail.digitalAsset;
            }
        } else {
            this.digitalAssets.push(e.detail.digitalAsset);            
        }
        
        this.digitalAssetEditorElement.setAttribute("digital-asset", JSON.stringify(new DigitalAsset()));
        this.digitalAssetListElement.setAttribute("digital-assets", JSON.stringify(this.digitalAssets));
    }

    public onDigitalAssetEdit(e) {
        this.digitalAssetEditorElement.setAttribute("digital-asset", JSON.stringify(e.detail.digitalAsset));
    }

    public onDigitalAssetDelete(e) {       
        if (e.detail.digitalAsset.id != null && e.detail.digitalAsset.id != undefined) {
            this.digitalAssetEditorElement.setAttribute("digital-asset", JSON.stringify(new DigitalAsset()));
            this.digitalAssets.splice(this.digitalAssets.findIndex(o => o.id == e.detail.digitalAsset.id), 1);
            this.digitalAssetListElement.setAttribute("digital-assets", JSON.stringify(this.digitalAssets));
        } else {
            for (var i = 0; i < this.digitalAssets.length; ++i) {
                if (this.digitalAssets[i].digitalAssetUrl == e.detail.digitalAssetName)
                    this.digitalAssets.splice(i, 1);
            }

            this.digitalAssetEditorElement.setAttribute("digital-asset", JSON.stringify(new DigitalAsset()));
            this.digitalAssetListElement.setAttribute("digital-assets", JSON.stringify(this.digitalAssets));
        }
    }
    
    private onTabSelectedIndexChanged(e) {
        this._router.navigate([
            "idea",
            "edit",
            this.ideaId,
            "tab",
            e.detail.selectedIndex]);
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._titleElement.addEventListener("click", this.onTitleClick);
        this.addEventListener(tabsEvents.SELECTED_INDEX_CHANGED, this.onTabSelectedIndexChanged);

        this.addEventListener(linkActions.ADD, this.onLinkSave);
        this.addEventListener(linkActions.EDIT, this.onLinkEdit);
        this.addEventListener(linkActions.DELETE, this.onLinkDelete);

        this.addEventListener(digitalAssetActions.ADD, this.onDigitalAssetSave);
        this.addEventListener(digitalAssetActions.EDIT, this.onDigitalAssetEdit);
        this.addEventListener(digitalAssetActions.DELETE, this.onDigitalAssetDelete);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._titleElement.removeEventListener("click", this.onTitleClick);

        this.removeEventListener(linkActions.ADD, this.onLinkSave);
        this.removeEventListener(linkActions.EDIT, this.onLinkEdit);
        this.removeEventListener(linkActions.DELETE, this.onLinkDelete);

        this.removeEventListener(digitalAssetActions.ADD, this.onDigitalAssetSave);
        this.removeEventListener(digitalAssetActions.EDIT, this.onDigitalAssetEdit);
        this.removeEventListener(digitalAssetActions.DELETE, this.onDigitalAssetDelete);
    }

    public async onSave() {
        const idea = {
            id: this.ideaId,
            ideationId: this._ideationSelectElement.value,
            name: this._nameInputElement.value,
            htmlBody: this.htmlBodyEditor.text,
            htmlDescription: this.htmlDescriptionEditor.text,
            digitalAssets: this.digitalAssets,
            links: this.links
        } as Idea;
        
        await this._ideaService.add(idea);

        this._ideaHub.ideaAddedOrUpdated(idea);

        this._router.navigate(["idea","list"]);
    }

    public async onDelete() {        
        await this._ideaService.remove({ id: this.ideaId });
        this._router.navigate(["idea","list"]);
    }

    public onTitleClick() {
        this._router.navigate(["idea", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "idea-id":
                this.ideaId = newValue;
                break;

            case "tab-index":
                this.customTabIndex = newValue;
                break;
        }        
    }

    public ideaId: number;

    public htmlBodyEditor: EditorComponent;

    public htmlDescriptionEditor: EditorComponent;

    public customTabIndex: number;

    private links: Array<any> = [];

    private digitalAssets: Array<any> = [];

    public get tabsElement(): HTMLElement { return this.querySelector("ce-tabs") as HTMLElement; }

    public get linkEditorElement(): HTMLElement { return this.querySelector("ce-link-edit-embed") as HTMLElement; }
    public get linkListElement(): HTMLElement { return this.querySelector("ce-link-list-embed") as HTMLElement; }
    public get digitalAssetEditorElement(): HTMLElement { return this.querySelector("ce-digital-asset-edit-embed") as HTMLElement; }
    public get digitalAssetListElement(): HTMLElement { return this.querySelector("ce-digital-asset-list-embed") as HTMLElement; }

    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".idea-name") as HTMLInputElement; }
    private get _htmlBodyInputElement(): HTMLDivElement { return this.querySelector(".idea-html-body") as HTMLDivElement; }
    private get _htmlDescriptionInputElement(): HTMLDivElement { return this.querySelector(".idea-html-description") as HTMLDivElement; }
    private get _ideationSelectElement(): HTMLSelectElement { return this.querySelector("select") as HTMLSelectElement; }
}

customElements.define(`ce-idea-edit`,IdeaEditComponent);