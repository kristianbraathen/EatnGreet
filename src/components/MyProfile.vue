<template>
    <div>
        <h1>My Profile</h1>
    </div>
    <button @click="openNewEntry">New Entry</button>
    <NewEntry v-if="showAddEntry" @close="showAddEntry = false"></NewEntry>
</template>

<script lang="js">
    import { defineComponent } from 'vue';
    import NewEntry from './NewEntry.vue'

    export default defineComponent({
        components: {
            NewEntry
        },
        directives: {
        },
        filters: {
        },
        props: {
        },
        data() {
            return {
                showAddEntry: false
            }
        },
        computed: {
        },
        watch: {
        },
        beforeCreate() {
        },
        created() {
        },
        beforeMount() {
        },
        mounted() {
        },
        updated() {
        },
        activated() {
        },
        deactivated() {
        },
        beforeDestroy() {
        },
        destroyed() {
        },
        methods: {
            openNewEntry() {
                this.showAddEntry = true;
            }
        },
    });
</script>

<style>
    .modal {
        position: fixed;
        z-index: 1;
        left: 0;
        top: 0;
        width: 100%;
        height: 100%;
        overflow: auto;
        background-color: rgb(0, 255, 144);
        background-color: rgba(0,0,0,0.4);
    }
</style>