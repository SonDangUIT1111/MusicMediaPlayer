//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusicMediaPlayer.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlayList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlayList()
        {
            this.Songs = new HashSet<Song>();
        }
    
        public int PlayListId { get; set; }
        public string PlayListName { get; set; }
        public Nullable<int> SongCount { get; set; }
        public int OwnerId { get; set; }
    
        public virtual UserAccount UserAccount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Song> Songs { get; set; }
    }
}