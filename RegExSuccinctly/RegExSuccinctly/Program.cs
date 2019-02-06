using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegExSuccinctly
{
    class Program
    {
        static void Main(string[] args)
        {
            // An example of parsing phone numbers and reformatting them to clean data.
            // Guts are commented out.
            #region CleaningData

            // First, here's the regex pattern we use broken down:
            // Might begin with a one and a dash, space or dot, but we don't
            // want to keep them: (?:1[-\s.])?  -- the ?: is for a non-stored group.
            // Possibly followed by a left parenthesis: (\))?
            // Then the area code: (?<areacode>\d{3})
            // If group one has left parethesis, get right: (?(1)\))
            // Space, dash or period [-\s.]?
            // optional space \s?
            // Phone number split into two groups:
            // (?<prefix>\d{3})[-\s.]
            // (?<number>\d{4})

            //string[] PhoneNumbers = { "1-215-555-1212", "(610) 867-5309",
            //              "484/ 555-1234", "203.514.2213" };
            //string CleanedPhone = "";
            //Regex thePhone = new Regex(@"(?:1[-\s.])?(\()?(?<areacode>\d{3})(?(1)\))[-\s.\/]?\s?(?<prefix>\d{3})[-\s.](?<number>\d{4})");
            //Regex FinalFormat = new Regex(@"(\d{3}) \d{3}-\d{4}");
            //foreach (string OnePhone in PhoneNumbers)
            //{
            //    CleanedPhone = thePhone.Replace(OnePhone, "(${areacode} ${prefix}-${number}");
            //    if (CleanedPhone == OnePhone)
            //    {
            //        // Nothing changed
            //        if (FinalFormat.IsMatch(OnePhone))
            //        {
            //            // Phone number is already formatted as expected.
            //        }
            //        else
            //        {
            //            // The phone number string we got didn't match pattern and is not
            //            // already in our final format, should probably log for manual review.
            //            // Then we can modify our pattern to fit and clean more inputs based on
            //            // what we see.
            //        }
            //    }
            //    else
            //    {
            //        // Update the phone number database
            //    }
            //}
            #endregion

            // I wrote this code as a breif exercise:
            #region RemoveDoubleSpaces
            Regex RemoveDoubleSpace = new Regex(@"\s{2,}");

            // In this region some random fanfiction is assigned to a variable called "fiction"
            #region BadFiction
            // Disclaimer, I did not write or even read this, so I'm not really qualified to call it bad.  I just copied and pasted it to see if I could operate on it.
            // Credit (and any blame) goes to the author and it can be found here https://smashboards.com/threads/super-smash-bros-odyssey.35207/
            //
            string fiction = @"
Act 1: Not Again

In the heart of the Mushroom Kingdom, very close to Princess Peach's castle, lied the home of the most critically acclaimed and prominent idols in all the land. This was the humble adobe of none other than Mario Mario. His tales of endless heroism were known throughout his world, as he was best known for foiling the twisted plots of the diabolical King Bowser Koopa and saving the fair Princess Peach Toadstool.

During the mid-afternoon, Mario headed toward his mailbox in the front yard to pick up the mail that had just been delivered. He rummaged through a stack letters, noting nothing out of the ordinary.

'Bills...bills...credit card offers... bills...' Mario shuffled to one particular envelop which promptly exploded as soon as he touched it. 'WOOAAH!!Bullet Bill by mail...I'll get you, Wario...' He growled through gritted teeth as he continued shuffling through his delivery. 'Titillated Toadettes resubscription?' Mario raised an eyebrow of confusion when he came upon this piece of postage. 'I need to tell Luigi to stop using my address...'

Eventually, he stopped on one particular letter: a pink-enveloped parcel with a crowned golden mushroom stamp in the upper-left corner.This type of envelop only came from one person...

'A letter from Peach...' Mario hummed with a nod as he slid a finger through the seal, opened the envelop, and read the letter.

Dear Mario,

Please come to the castle. I've baked a cake for you! 

Yours truly,
Princess Toadstool(Peach)

'I was just in the mood for one of her cakes, too!' Mario said satisfactorily, hungrily rubbing his stomach.

Mario bought his mail into his house and took the nearest warp-pipe to Princess Peach's castle. Her home was looking just as grand and splendid as ever, with birds happily chirping, rabbits playing in the hedge maze, the waterfall flowing pure and clean as always.

Still with the letter in hand, Mario fished it out of his pocket and took a look at it.He couldn't help but feel like something was off about this whole situation, involuntarily raising an eyebrow as his eyes skimmed over the letter once again.

'Why does this feel so ridiculously familiar? In fact, why did Peach send me a letter instead of calling me?' Mario suddenly wondered. 'Eh, maybe I'm thinking too hard. It might be a special occasion or something.'

Shaking off his suspicions, Mario ran to the front door and let himself into the castle.

Without warning, accompanied by a threatening fanfare, a booming voice echoed throughout the main lobby of the castle.

'WELCOME!! NO ONE'S HOME!! NOW SCRAM-- AND DON'T COME BACK!! GWA HA HA!!'

Suddenly, it hit him: this was exactly how his greatest adventure yet had started: Super Mario 64!The same exact letter...the same exact greeting...It all came rushing back into Mario's memory.

'Not again... I have GOT to stop falling for Bowser's stupid tricks! I can't believe I fell for a rehash...' Mario sighed, disappointed in himself and Bowser's lack of creativity in his latest scheme. 'Well, time to get to work.'

All this time, Mario hadn't even stepped out of the doorway. He took nearly two steps and was immediately greeted by Toad, who wasted no time dealing Mario a heavy dose of incessant complaining.

'Mario! This is terrible! The princess was kidnapped by Bowser again! What are we going to do? WHAT ARE WE GOING TO DO?!! WAAAAAAAAAHHHHH!!' He bawled.

Mario wasn't too upset before, but seeing this display of sheer weakness set him off. Toad was supposed to be guarding the castle, and in the event of an impending kidnapping, he was to notify Mario ASAP to promptly save the day as he usually would. Instead, he finds Toad bawling in a corner, not even attempting to do anything about what just happened. It was things like these that Mario just couldn't tolerate.

'You're are so useless, it's unbelievable!' Mario screamed, seething with rage. 'I'll tell you what we're going to do: NOTHING!! I'm going to find Bowser, beat him within a nanometer of his life, rescue Peach, and tell her to get rid of you! And until then, you will shut the hell up, stand in a corner, violate yourself with your own fist, and smile about it.'

Toad just stood there. He had never seen Mario so enraged before. A stunned, sad, and hopeless look was plastered on Toad's face.

'What are you waiting for?! Get over there!' Mario furiously demanded, sending a boot to Toad's backside and a few fireballs for good measure.

'AAAACK!! OKAY, OKAY!!' Toad cried as he frantically ran to the farthest corner and dropped his pants. 

'And if I find out you moved from that spot...' Mario wasn't even able to complete his threat. His blinding rage clouded his creativity. 'Don't...move...from that spot.'

He finally left Toad to his fate and began his trek to the final battle stage. Since he sensed that since Bowser hadn't fooled around with the Power Stars this time around, he could just go straight to the end.

Not too long after he set out, he reached his destination without many hardships; none at all, in fact.He jumped down the final warp-pipe and prepared for an arduous battle.Much to his surprise, the only thing that awaited him was an empty, star - shaped platform.


'What...? I left this place like this years ago!' He realized as he glanced around, expecting this to be a trick.


Mario then noticed a lone, yellow note in the middle of the field.He walked up to it and picked it up off the ground.With a red watermark in the shape of a very familiar head, he knew exactly who it was from.It read: 


To the Fattest, Dumbest Plumber in all of Mushroom Kingdom,

You really are an idiot.I really meant it when I said nobody's home. You're really never gonna find me, meaning you'll never rescue Peach, so I suggest you get really used to rescuing yourself every morning and night, if you know what I mean... GWA HA HA!!


Get it ? It's funny because I'm playing on the idea of 'rescuing'.You rescue Peach from danger, she probably rescues you from...you know...But this time...


Uh oh... ...running out of room to write...I'll just end this with the laugh.


GWA HA HA!!

From the Illest of the Ill, the Baddest of the Bad, and the Evilest of the Evil,
King Bowser Koopa


P.S.GWA HA HA!!

Words could not describe how angry Mario was at that exact point in time.He crumpled the note in one hand, which lit ablaze and subsequently fried the letter.


Figuring there was nothing left to do; he jumped off the star-shaped field and ended up back in the castle courtyard.With no leads on where Bowser could be, he he could only take a seat in the grass and ponder about who could help him now. He sat with his legs crossed and his palms resting on his knees.

'Alright, time to calm down and think... Bowser couldn't have gotten that far. He moves pretty slow and he's not exactly inconspicuous, so someone had to have seen him pass by. But who...' Before he could even finish his thought, the answer hit him, causing him to jump up in excitement. 'Yes! He could help me!'

Mario dashed toward the courtyard cannon and hopped in, preparing to blast off toward the roof of the castle.Carefully aiming himself, he shot himself to the top, flawlessly landing right by the sleeping character he knew could help him.

'I knew it...' He mumbled in a low voice. 'Hey, wake up!' Mario said, nudging the resting shoulder. 'Yoshi! Wake up!'

With a groggy stir, Mario's faithful companion, Yoshi, uncurled himself from his sleeping position and rose to his feet, stretching to wake his body up.

'I can't believe you're still here after all this time. Don't you ever go home?' Mario wondered.

'Yes, I head home from time to time... But I consider this my home away from home. I mean... it's quiet, it's peaceful, and I have a perfect view of the sunset... What more could I want?' Yoshi asked, raising his arms in question. 'So... what can I do for you? Need a hundred extra lives again?'

'Not this time. I need is some info. Bowser kidnapped Peach again and he's probably manipulating her as we speak.' Just then, Mario let out a tired and disappointed sigh. 'I really need to have a talk with her. We went over this a thousand times... How safe can you be when you have Toad, who curls up in a corner, cries, and fists himself, guarding your castle?'

'He... fists... himself?' Yoshi asked with a face screwed up in confusion, not quite understanding what Mario meant.

Back in the corner of Peach's castle...

'Ugh... How does Mario expect me to... Wait... WAIT!!' A sickening squish was heard. 'AAAHHHHHHHHH!!'

Returning to the roof...

'Don't even try to think about it...' Mario shook his head. 'Anyway, I know you had to have seen something before you fell asleep.'

'As a matter of fact, I did. I actually saw Bowser flying into the sky in his Krazy Klown Kar just when I came to the roof. Later on, he jumped off and fell through a warp-pipe.' Yoshi recalled. 'He did have a large sack slung over his shoulder, but I didn't think anything of it. I just figured he was taking it easy for once. I should've known better...'

'Do you remember where the pipe was?' Mario asked.

'Yes, indeed. Look straight ahead and turn about seventeen degrees eastward.' Yoshi instructed.

Mario just turned to Yoshi, giving him an annoyed look.

'Are you kidding me? You know I don't have an innate sense of direction like you Yoshis do...' Mario muttered.

'A thousand pardons... Just follow the egg, then.' Yoshi said. 

He then popped out an egg and grabbed it with his throwing arm.After a brisk wind up, he sent the egg flying towards the pipe he was talking about. It was roughly about three hundred yards south of the castle.

'What an arm...' Mario said in awe. 'I've been wondering this for a long time now: how can you throw like that?'

'Well, it's actually impossible to throw an egg that far. The only way to get it that far is to know the truth.' Yoshi explained.

'And the truth is...' Mario inquired.

'There is no egg.' Yoshi said and smiled. 

'Alright... No more movies from Earth.' Mario replied. 'Anyway, thanks for the info. I'm going to go home to prepare for the journey and set out tomorrow. You want to help me out? It'll be just like our first adventure together!'

'I would go, but I have to stay with that bratty kid of yours. Ever since I saved him from Baby Bowser, he's been hanging on my tail and riding me every waking moment!' Yoshi said angrily.

'Oh yeah... My... son...' Mario stammered. 'Well, if it makes you feel any better, me and Peach appreciate you watching him against your will. Just to let you know, you'll be doing this for a loooooooooong time.' Mario said. 

'Wait until I settle down with Birdo... You'll see... I'll be sure to send all of my children your way.' Yoshi vengefully promised.

'You know, Yoshi...' Mario began. 'Birdo is a man... named Ostro. He has a serious case of gender confusion.'

'Those are rumors, Mario! Nothing but awful, awful rumors!' Yoshi grew offended, refusing to believe what Mario was saying.

'Okay... Whatever you want to believe.' Mario sighed, leaving it to Yoshi to find out the horrible truth on his own. 'I'll see you later.'

'Good luck!' Yoshi called out just as Mario jumped into the warp-pipe leading home.

Mario returned home almost within an instant.The sun was setting fast; when he jumped out, the sun just sank below the horizon, leaving the sky a pretty shade of orange blending into a nightly-blue.The scene on the home-front seemed quiet enough.As he neared closer and closer to his front door, though, he noticed it was slightly opened.

'I know I locked the door before I left.' Mario murmured to himself.

He cautiously stepped through the front door.It was eerily dark as he made his way past his living room.A strange, blue glow radiated on the hallway walls. He looked through the doorway and found the TV on at a low volume. Mario didn't like what he saw on his screen, though.

'Toadettes Gone Wild?!' Mario gasped as he dove at the remote on the arm of his couch and shut off his television.

That's when he heard heavy breathing. It was faint, but he could tell it was coming from upstairs. Briskly, but quietly, he made his way upstairs and followed the gradually loudening breaths to his bathroom. The door was closed, just as he left it earlier, but by the glow of the light from under the door, he knew someone was in there. Mario put his ear to the door, hoping to figure out who this intruder was.

'Ooohhh yes... oooohhh yeah... ooohhh Mama Mia...' A familiar voice chanted from the other side of the door.

'Of course...' Mario sighed as he backed away from the door and assumed an attack position.

Suddenly, Mario kicked the door down and caught none other than his younger, taller brother, Luigi Mario, with his pants down, literally.In one hand: Titillated Toadettes magazine.In the other... ...that'll be left to the imagination to decide...

'Luigi... How many times must I tell you not to watch your filthy porn here?! Huh? How many times?' Mario angrily demanded.

'Sorry... But it's not like you were here this time...' Luigi muttered, pulling up his pants, extremely embarrassed.

'That's not the point! This is my house! You broke in and left the door wide open! You know I can have you arrested?!' Mario shouted. 'It's bad enough Bowser kidnapped Peach again and Toad didn't do anything about it again. You being here just makes my day worse.'

'Bowser got to Peach again? Unbelievable...' Luigi sighed, shaking his head. 

'Is it really that hard to believe? What I find bewildering is that we're still depending on him to keep the castle safe.' Mario said.

'That is incredibly amazing. You'd think we'd all know better by now... Well, we'll fix that after we save Peach.' Luigi assured him.

'Wait, wait... Wait a second... We'll? As in: we will? As in: you and I will? Think again, Mr. I-masturbate-in-my-brother's-bathroom. I'm not working with you again. No. You cramp my style in ways impossible to imagine.' Mario said.

'Well, get over it! Whatever you say isn't stopping me. I'm coming too, whether you like it or not.' Luigi shot back. 'I'm tired of sitting around in that stupid mansion all day. There are still ghosts there! They used to be scary, but now they're just annoying! I can't watch my... tapes... over there because they always have some smart-alec side-comment! And I never have any privacy! They're always watching me! ALWAYS!!' He emphasized. 'It wouldn't be so bad if I had visitors or something, but even Daisy stopped visiting.'

'Speaking of Daisy... Your relationship with her isn't happening because she actually likes you. She's only with you as a favor to me. You see, Donkey Kong kidnapped her for no reason way back when he was into kidnapping helpless females. It was probably because he could never get to Peach, since Bowser was always after her. I guess Daisy seemed like the next best thing.

'After I saved her, she was ready and willing to do anything for me. And I do mean anything.' Mario stressed. 'I had a few things in mind, but I decided to be a good brother. I tried to get her interested in you so you could develop your own identity. You've been living in my shadow for way too long, so I figured with the right woman, you could make something of yourself. I guess you like the shade because you're still living in my shadow. People are starting to forget your name. They just refer to you as 'the green Mario' or 'Mr. Greenie'. Even I'm starting to forget. How can you live with being nothing more than my eternal understudy?'

Luigi thought hard about some type of retribution statement, but Mario's words were unfortunately true. No one knew who he was anymore. The only thing they knew about him was that he was a coward. Luigi could admit that he's more cowardly than the average person, but when it came down to it, Luigi had courage deep down inside of him that could easily rival, if not best, Mario's.

With a clutched fist and gritted teeth, Luigi just took Mario's words to heart and just began mentally preparing for when he'd be able to prove not only his brother's assumptions, but everyone else's wrong.

'I am going to go home now.' Luigi simply stated, opting to leave instead of starting an argument.

'Best news I heard all day.' Mario nodded, approvingly.

'Don't think I won't be back, though, because I shall return! Tomorrow, bright and early, we're setting out to get Peach back. Be ready.' Luigi said and headed out of the house.

Mario waited until he heard his front door close.

'Don't worry. I'll be ready. I won't be here when you are, though.' He replied to a nonexistent Luigi while heading up to his bathroom to prepare for bed.

After preparing for bed, Mario headed toward his room and jumped under his covers, getting rested up for what he anticipated to be one hell of a day tomorrow.

'I don't even know why I'm in bed so early. It's only Bowser I'm dealing with. This'll be just like every other episode I been through.' Mario thought to himself, aloud. 'Eh, I'm tired anyway. A good night's sleep should do me good.'

With that and the sound of his music box softly chiming the Piranha Plant's Lullaby, he dozed off and fell asleep. Little did he know that this trip to save Peach from Bowser's clutches would be nothing like any adventure he had ever gone on before...
____________________________________________________________
____________________________________________________________

Act 2: And History Repeats Itself

In the legendary land of Hyrule, there was a legendary tale passed down from generation to generation.This was the tale of the Hero of Time, Link, and how he saved Hyrule and the Princess of Destiny, Zelda, from the tyrannical rule of the King of Evil, Ganondorf.

What the legend never spoke of was how Link, Zelda, and Ganondorf all became friends after the events of Ocarina of Time.

During one of the majestically beautiful days in Hyrule, in the courtyard of the grandiose palace, Link and Ganondorf reminisced about their past.Of course, with them both being incredibly egotistical, their conflicting memories dead - locked them into an unending argument.

'But seriously, G-dorf, I was the freakin' man! In fact, I still am! You know it. I know it. Zelda knows it. All of Hyrule knows it. The list just goes on!' Link cockily boasted.

'Oh, word?' Ganondorf said, letting Link's head inflate to epic proportions just so he could have the sole satisfaction of popping it.

'D***-straight, 'word'! You can't deny it! You know I coulda taken you out as a kid, but I decided to let you live. It'd mean more if I faced you man-to-man, know what I mean?' Link continued.

'F*** outta here! Yo' a** wouldn't las' two seconds against me if you tried that s***. Remember when I blasted you after I asked you where Zelda was? Yo' a** flew back ten feet and you screamed like a lil' b****!' Ganondorf recalled, chuckling. 'Even when you 'grew up', you were still too wack to test my skills. You know god***-well you woulda never touched me if it weren't for those god*** Light arrows and that lil' b****, Navi. And even then, you still couldn't do the d*** thing by yo'self! Zelda had to hold me down so you could finish the job!'

'Bulls***! I can murk you easily without any of that!' Link shot back.

'But you still need a f***in' army-load of weapons to take me down!' Ganondorf reminded. 'Seriously, my man... a Megaton Hammer? A Hookshot? F***in' Deku nuts?! Yo' nothin' wit'out all that s***! I'ma say it again properly: You are nothing without your weapons. I don't need any of that s***! All I need is my sword and my transformation. And even those are unnecessary. It's all me, baby.'

'Alright, G-dorf, I got you. We're gonna do this! Me vs. you. Sword-to-sword and nothin' else.' Link said. 'I'ma show you you're just talkin' out your a**, as usual. You can't touch me! I'm the Hero of Time!' He announced as his fanfare, the beginning theme from A Link to the Past, blared throughout the relative silence of the courtyard.

'Are you kiddin' me? Who the f*** are you tryin' to impress wit' that 'Hero of Time' bulls***?' Ganondorf demanded. 'Seriously, kid. Yo' nothin'! Inside that body is the mind of a sorry-a** punk b**** who can't fight for s***. The only reason yo' a** ain't hiding under yo' momma's dress is because she and her weak-a** attire are six feet under my boots.'

Link was shocked by those words.He thought this was a relatively friendly challenge.But with that blatantly hate - filled insult, Ganondorf took it way too far.

Blind with a sudden rage, Link unsheathed his sword and charged at an unarmed Ganondorf.At this point, he didn't care if Ganondorf was defenseless. Link was aiming for the head.

Unfortunately for Link, Ganondorf was never defenseless.Reading Link's moves like a children's book, Ganondorf inconspicuously powered up a Warlock Punch.Just when Link was within prime hacking range, Ganondorf released all his energy into his right arm and sent it careening into Link's gut.

'HHHHHAAAAAAAAAAAHHHHHH!!!' Ganondorf screamed, connecting flawlessly with Link's midsection.

Ganondorf's fist literally got buried in Link's stomach.It hit so hard that the outline of his fist actually could be seen through Link's back. Then, in an explosive purple blaze of dark energy, Link was sent flying out of the courtyard, soaring through the air toward his home back in Kokiri Forest.

Link crashed through the roof of his home and broke through a table.He was incredible pain, as the wooden table he landed on didn't provide much cushioning, not to mention he was still reeling from the pain of that punch to the abdomen.

'Ugh... I can't believe he said that...' Link groaned, attempting to rise from under the rubble that used to be his table, but winced at his blinding pain and stayed put, closing his eyes.

What if he's right, Link suddenly thought to himself. What if I never had any help? Would I have been able to win?

Just as suddenly as his self - doubting thoughts began, he opened his eyes and sprung up into a sitting position.

'What the hell am I thinkin'? The Goddesses wouldn't let me go out like that!' Link confidently stated. 'Whatever hand I was dealt, I woulda won somehow. The same s*** applies now! I'ma head back over there and show Ganondorf just who the hell he's effin' with!'

After digging himself out of the mess he made and stretching the pain away as best as he could, he made his way out of the house.Just as he stepped onto the balcony, he noticed the Sage of the Forest, Saria, running toward the ladder leading to his front door.

'Link! You're back!' She sang, happily. 'Did you miss me?'

'Hell no!' Link stated, climbing down the ladder to Saria's level. 'The only reason I'm here is because me and Ganondorf got into some confrontations and he sent me flyin' over here. He thinks he's so great...We'll see how great he is when he feels my left-right combo.' He said, fiercely punching the air. 

He was unfortunately reminded of his unbearable pain when he dealt a hard left jab.

'Aww... aww... d***it... I shouldn't have done that...' Link groaned, holding his back and screwing up his face in agony.

'Just take it easy, Linky. Saria will make it all better.' Saria cooed as she sat Link down and began to ma** age Link's aching back.

Link thought about protesting, but let it ride.He was in pain but the motions were feeling pretty good at the moment.He was actually forgetting about his pain as he began to relax a little.

Sensing Link's relaxation as he let out a relieved sigh, Saria slowly moved her hands down Link's back, and eased her way around his lower back to his stomach to rub his belly. Feeling no resistance, Saria decided to move even further down, slowly inching her way toward Link's pride and joy.

And that's when Link reacted, promptly sending an elbow to Saria's forehead and jumping to his feet.

'OWW!!' She yelped. 'What are you? Gay?! I was trying to help you forget about the pain!' Saria shouted, rubbing her pained forehead.

'This is exactly why I'm never here! Your little a** is always hungry for sex and I want no part of it! What the hell is your deal? You're twelve!' Link shouted.

'I'm not a kid! I'm a lot older than you think! I'm sure I'm way older than your girlfriend, Zelda! Believe me!' Saria shot back.

'I don't give three d***s! In my eyes, you're just a horny-a** little girl that somehow became the Sage of the Forest.' Link said.

'If that's what you want to think, then fine! Be that way! Mido is more of a man than you'll ever be, anyway! I know he'll treat me like a woman.' She said as she merrily skipped over to Mido.

'I'm definitely never comin' back here again...' Link muttered. 'Whatever. It's time show Ganondorf what's really good.' He headed toward the exit leading to Hyrule Field.

When he entered the field, he immediately noticed the blackened sky and the booming thunder clamoring in the heavens. The air also felt colder as a chilling breeze ruled the air.It was as if the land was saying 'grimness is afoot'.

A frown began to form on Link's face after seeing Hyrule Field. He then pulled out his invaluable Ocarina of Time and played a tune that was carried by the winds far and wide: Epona's Song. A strong neighing was heard in the distance and within seconds, Link's trusted steed, Epona, galloped beside him and came to a stop. Link climbed onto Epona's back and motioned her to gallop toward Hyrule Castle.

His worst fears turned into a reality as he neared the castle grounds. The closer he got, the darker the sky became, and the more extravagant the thunder and lighting grew. Epona continued to gallop past the castle walls and through the once-lively Hyrule Castle Town.

'Whoa, Epona!' Link commanded as he slowed her to a stop, studying the castle that lied before them.

What was once a lovely and majestic castle was now a dark, intimidating, and deliciously evil-looking fortress.It floated on a single plot of land over a deep, boiling pit of lava.

'...and history repeats itself... God***it, Ganondorf.' Link muttered, his face slowly becoming more and more twisted with anger.

He dismounted his trusted steed and crossed the bridge of dark energy into the fortress.There were no obstacles to overcome as Link easily ran through to the main pillar of the fortress and traveled up the twisting stairs into the throne room where he knew Ganondorf waited.

Just as he thought, Ganondorf was there.He was standing over a black organ and was playing a dark and foreboding tune.After letting the final note drown out, he spun around, letting his cape flap loudly, and faced Link with a devious smirk. Link stared back with a fierce intensity and a face of determination absent of all smiles.

'What the hell are you doin', Ganondorf?' Link demanded. 'You know this isn't the way I wanted to settle this! D***it, man, I didn't even want to get into this at all! I wasn't serious! I thought we were s***-talkin' as usual! The only reason I charged you was because you said some real f***ed up s*** about my mom, and I know you weren't jokin' around.'

Ganondorf only shook his head, disappointed. 'You really are nothin' but a b****.' He taunted. 'Listen to you: tryin' to find a way outta this fight by talkin' that weak s***. I know you weren't serious. But I wanted to make it serious. You singin' all that bulls*** about way back when was pissin' me off. You need to understand that yo' nothin' but a punk-a** b****, son, not that 'Hero of Time' you have wet dreams about. I'ma show you, right now, what woulda happened if you were on your own.'

Just then, the skies outside of the castle somehow managed to grow even darker than before as Ganondorf held up his right fist.His piece of the Triforce, the Triforce of Power, pulsed a golden glow.Slowly, he grew bigger and took the form of a monstrous beast.His face grew out of proportion, changing its form into that of a mutated warthog. His hair also grew out long and fiery orange, resting on his bulging, muscled shoulders. Not only were his shoulders muscled, but the rest of his body grew to incredible proportions; particularly his arms and legs.In each hand, he held a devastating golden - bladed sword.

'No... Ganon... versus me with no Light arrows...' Link sighed as he unsheathed his Master Sword and attempted a defensive stance behind his shield. 

'YOU THINK THAT TIRED-A** SHIELD IS GONNA SAVE YOU?! HOLD THIS!' Ganon roared and swung viciously at Link.

With a heavy clang, Link was sent spiraling into one of the hard stone walls. He literally bounced off and landed with a thud into his stomach. This reawakened the searing pain in his gut and he stayed grounded.

'God***it... My stomach...' He complained, clutching his belly and agonizingly rolling to his back. 'I'm gonna get killed if I don't get outta here...' He slowly reached into his inventory and took out his ocarina.

'THAT'S RIGHT, B****! RUN! RUN BEFORE YO' WEAK-A** GETS KILLED!' Ganon taunted as he emitted a booming laugh.

Suddenly, a deep rumbling shook the floor beneath them. Link just lied there, letting himself get shaken relentlessly. Ganon, on the other hand, struggled to keep his balance. Link managed to rise to one knee to look at Ganon.He actually looked like he didn't know what was going on.

Finally, the rumbling stopped as a red warp-pipe exploded from the ground. And from that pipe jumped another figure: a large-framed figure, nearly as tall as Ganon and definitely wider, but not as powerful.He landed, shaking the ground and holding a large brown sack over his shoulder.

'Bowser's in the house!' Bowser announced, holding a fist of triumph in the air. 

'IT'S ABOUT TIME YO' HEAVY-A** GOT HERE!' Ganon shouted.

'Hey, it isn't easy getting to Hyrule by warp-pipe! I got lost a couple of times...' Bowser admitted.

'WHATEVER! I GOT ZELDA LOCKED UP IN THE TOWER! YOU DEFINITELY GOT PEACH, RIGHT?!' Ganon screamed.

'Do you know who I am? It's in the bag!' Bowser replied. 

He tossed the large bag onto the ground. Loud shrieks were heard sounding off from the inside.Without a doubt, it was Peach on the inside.Ganon and Link looked at each other disappointedly.

'Wow, B-Boss... Jus'... wow... BUT GOOD S***, SON! IT'S ON, BABY!' Ganon bellowed exuberantly as thunder and lightning went off with extravagant force.

'It's about as on as an unplugged lightbulb!' Link suddenly exclaimed, jumping up to his feet, but slightly wincing from the pain. He then whipped out his bow and arrow and armed the arrow with a grandiose Super Bomb.He aimed it at Ganon and Bowser, alternately. 'Pass Peach and Zelda before I get serious.'

Bowser and Ganon glanced at each other briefly. Suddenly, they burst out laughing, giving each other a pound as if they accomplished something. Link struggled to hold his composure as Ganon and Bowser continued to belittle him with their riotous laughter.

''Before he gets serious', he says! With medieval weapons!' Bowser could barely even talk without bursting into laughs; holding one hand over his stomach and having the other wipe a stray tear from his eye. 'You gotta be kidding me!'

'I kid you not! I won't hesitate to set it off if you don't hand 'em over!' Link threatened.

Without even saying anything, Bowser spit flames at Link's bow and arrow. Everything but the bomb disintegrated on contact. The Super Bomb was left to drop to the floor and hit the ground with a hard 'thunk'. Immediately, the bomb detonated with colossal force right at Link's feet. Ganon and Bowser were blown back a little by the shockwave it sent out, but Link was blown sky - high, screaming into the wild blue and out of sight.

Ganon walked up beside Bowser and patted him on the shoulder, approvingly.

'What a punk...' Bowser sighed.

'THAT'S WHAT HAPPENS WHEN YOU TALK GREEZY AND CAN'T BACK YOUR WORDS UP!' Ganon stated. 'THAT MOFO DOESN'T KNOW HOW TO STAY OUTTA PEOPLE'S BIZ, THOUGH! HE'LL BE BACK TO START SOME S*** AGAIN!'

'Yeah, I know. And I'm d***-sure Mario will find a way here, too, somehow. You know, if they team up, we might be in for some turbulence.' Bowser realized.

'THAT'S NOT EVEN AN ISSUE, SON! WIT' ME IN THIS FORM, NEITHER OF THEM CAN DO A D*** THING! IT'LL BE LIKE I'M AN EARTHQUAKE AND THEY'RE STANDIN' ON THE FAULT LINE: GUARANTEED CASUALTIES!' Ganon claimed in an unbearably loud roar.

Bowser could only cover his ears to try to drown out the earsplitting noise, but that barely did a thing.

'We're gonna be spending a lot of time together... I hope you're not planning on staying like that for the entire time. I'm seriously gonna go deaf hanging with you.' Bowser said with his hands over his ears.

'NAH! I'M 'BOUT TO CHANGE--' Ganon started.

'Ganondorf, seriously!' Bowser interrupted, trying to shout over the booming roar.

'MY BAD! My bad.' Ganon apologized as he transformed back into Ganondorf.

'Thank you, God... Urrgh... My ears are still ringing.' Bowser groaned, holding a hand over his ear like he was in pain. 'I know what'll fix it up, though: Zelda and Peach screaming in terror. I'm telling you: there's nothing like hearing a helpless dame crying for help.' He admitted, grabbing the bag Peach was still in and smiled as muffled, terrified cries broke through the cloth. 'It's almost musical.'

'I hear that, man. Let's get this party started!' Ganondorf suggested as he led the way to the tower where Zelda was being held captive.

Meanwhile...

Link was just beginning his decent toward the ground.As he bulleted toward the floor, he recognized the terrain below him.

'Lake Hylia!' Link exclaimed. 'At least the landin' won't be so bad. Water is better than the ground.'

As he fell closer and closer to the ground, he noticed that there was a serious lack of water in the Lake Hylia area. Suddenly, he remembered that the last time Ganondorf took over Hyrule, Lake Hylia was emptied out. Since he was mimicking the same events, Lake Hylia was once again devoid of almost all essential lake material.

'This does not bode well...' Link muttered. 

Seconds later, he slammed into the ground with the enough force to crack three three-foot thick slabs of concrete three times. Somehow, he was still alive, even after the impact of the landing. The saddest part was that he landed right near the entrance of the Water Temple, which was preceded by a deep pool of water.Link simply looked to his side and uttered a laughably weak giggle.

'Whoooooa, there goes my consciousness...' Link noticed as his vision began to get progressively blurry and dark. 'Hopefully, things start lookin' up when I come back to life...'

And with that thought, Link passed out. Of course, he had no idea just what he'd be getting into after he regained his consciousness, though...'";

            #endregion

            int occurrences = RemoveDoubleSpace.Matches(fiction).Count;
            string result = RemoveDoubleSpace.Replace(fiction, @" ");
            Console.WriteLine($"{occurrences} double spaces were removed!");
            Console.ReadLine();
            #endregion
        }
    }
}
