<template>
    <div>
        <h2>Legg til: </h2>
        <form class="form" @submit.prevent="newdish">
            <label for="description">Describe your dish</label>
            <input type="text" id="description" v-model="description" />
            <label for="date">Dato</label>
            <input type="date" id="date" v-model="selectedDate" />      
            <label for="time">Time</label>
            <input type="time" id="time" v-model="time" />
            <label for="vegetarian">Vegetarian</label>
            <input type="checkbox" id="vegetarian" v-model="vegetarian" />
            <label for="allergens">Any Allergens?</label>
            <input type="text" id="allergens" v-model="allergens" />
            <button type="submit">EnterDish</button>
        </form>
</div>

</template>

<script lang="js">
   
    import { defineComponent } from 'vue';
    import DatePicker from 'vue3-datepicker';
    import { ref,computed } from 'vue';


    export default defineComponent({
        components: {
            DatePicker
        },
        directives: {
        },
        filters: {
        },
        props: {
        },
        data() {
            return {
                description: '',
                allergens: '',
                time: '',
                selectedDate: '',
                vegetarian: false,
                showModal: false,


            }
        },
        setup() {
            const selectedDate = ref(new Date());
            const description = ref('');
            const allergens = ref('');
            const time = ref('');
            const vegetarian = ref(false);
            console.log(selectedDate)
            let userJson = localStorage.getItem('user');
            let user = null;


            if (userJson) {
                user = ref(JSON.parse(userJson));
                console.log(user);
            } else {
                console.error('User is not logged in');
            }

            function userNotLoggedIn() {
                console.error('User is not logged in');

            }
            async function newdish() {
                console.log(selectedDate)
                if (!user || !user.value) {
                    console.error('User is not logged in');
                    return;
                }
                try {

                    const response = await fetch('https://localhost:7218/api/Newentry',
                        {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json',
                                'userId': user.value.id
                            },
                            body: JSON.stringify({
                                description: description.value,
                                allergens: allergens.value,
                                time: time.value,
                                date: selectedDate.value,
                                vegetarian: vegetarian.value,
                                userRegId: user.value.id
                            })
                        });
                    console.log(response);


                }
                catch (error) {
                    console.error('Error:', error);
                }
            }

            return { user, userNotLoggedIn, newdish, description, allergens, time, vegetarian, selectedDate };
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
        methods: {},
            
    });
</script>

<style>
</style>